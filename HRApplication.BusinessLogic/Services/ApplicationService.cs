using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using HRApplication.BuisnessEntities.Enums;
using System.Threading.Tasks;

using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

using System.Configuration;
using System.IO;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HRApplication.BusinessLogic.Services
{
    public class ApplicationService: IApplicationService
    {

        private readonly HRAppDBContext _context;
        private IConfiguration _configuration;

        public ApplicationService(HRAppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AddNewApplication(Guid JobOfferId, IFormFile CV)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ApplicationStatusHistory statusHistory = new ApplicationStatusHistory
                    {
                        Id = Guid.NewGuid()
                    };


                    Applications application = new Applications
                    {
                        CreatedById = Guid.Parse("17496B8A-8E4E-4E8A-8099-101998018B03"), //TODO: Not mock user
                        CreateOn = DateTime.Now,
                        Id = Guid.NewGuid(),
                        OfferId = JobOfferId,
                        ApplicationStatusHistoryId = statusHistory.Id,
                        CurrentApplicationStateName = ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.New].name
                    };

                    statusHistory.ApplicationStateId = ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.New].id;
                    statusHistory.ApplicationId = application.Id;
                    statusHistory.Date = application.CreateOn;

                    string fileName= "cv" + application.Id.ToString();
                    string connectionString = _configuration.GetConnectionString("AzureBlob");
                    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                    var section = _configuration.GetSection("Azure");
                    string containerName = section.GetValue<string>("ContainerName");
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);
                    blobClient.Upload(CV.OpenReadStream());

                    application.CvfileName = fileName;
                    _context.Applicationss.Add(application);
                    _context.ApplicationStatusHistory.Add(statusHistory);

                    _context.SaveChanges();

                    Offers offer = _context.Offers.Where(x => x.Id == JobOfferId).Include(x=>x.CreatedBy).First();
                    await SendNorificationToHRMember(offer.CreatedBy.Email, offer.CreatedBy.FirstName + " " + offer.CreatedBy.LastName);

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckIfOfferIsApproved(Guid userId, Guid jobOfferId)
        {
            var offers = from offer in _context.Offers
                         join application in _context.Applicationss on offer.Id equals application.OfferId
                         where application.CreatedById == userId && offer.Id == jobOfferId && application.CurrentApplicationStateName == ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.Approved].name
                         select offer;

            return offers.Count() != 0;
        }

        public bool CheckIfOfferIsNew(Guid userId, Guid jobOfferId)
        {
            var offers = from offer in _context.Offers
                         join application in _context.Applicationss on offer.Id equals application.OfferId
                         where application.CreatedById == userId && offer.Id == jobOfferId && application.CurrentApplicationStateName == ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.New].name
                         select offer;

            return offers.Count() != 0;
        }

        public bool CheckIsOfferIsAlreadyApplied(Guid userId, Guid jobOfferId)
        {
            var offers = from offer in _context.Offers
                         join application in _context.Applicationss on offer.Id equals application.OfferId
                         where application.CreatedById == userId && offer.Id == jobOfferId
                         select offer;

            return offers.Count() != 0;
        }

        public async Task DeleteApplication(Guid JobOfferId)
        {
            Guid userId = Guid.Parse("17496B8A-8E4E-4E8A-8099-101998018B03"); //TODO: not mock user

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var appToDelete = _context.Applicationss.Where(x => x.CreatedById == userId && x.OfferId == JobOfferId).First();

                    var statusesToDelete = _context.ApplicationStatusHistory.Where(x => x.ApplicationId == appToDelete.Id);

                    string fileName = "cv" + appToDelete.Id.ToString();
                    string connectionString = _configuration.GetConnectionString("AzureBlob");
                    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                    var section = _configuration.GetSection("Azure");
                    string containerName = section.GetValue<string>("ContainerName");
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    containerClient.DeleteBlobIfExists(fileName);

                    _context.ApplicationStatusHistory.RemoveRange(statusesToDelete);
                    _context.Applicationss.Remove(appToDelete);

                    _context.SaveChanges();



                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }
        }

        public void EditApplication(Guid JobOfferId, IFormFile CV)
        {
            Guid userId = Guid.Parse("17496B8A-8E4E-4E8A-8099-101998018B03");

            var appToEdit = _context.Applicationss.Where(x => x.CreatedById == userId && x.OfferId == JobOfferId).First();

            string fileName = "cv" + appToEdit.Id.ToString();
            string connectionString = _configuration.GetConnectionString("AzureBlob");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            var section = _configuration.GetSection("Azure");
            string containerName = section.GetValue<string>("ContainerName");
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.DeleteBlobIfExists(fileName);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            blobClient.Upload(CV.OpenReadStream());
        }

        public List<Offers> GetAllJobOffers()
        {
            return _context.Offers
                    .Include(x=>x.ContractType)
                    .Where(x => x.EndDate >= DateTime.Now && !x.IsArchived)
                    .ToList();
        }

        public List<Offers> GetAlreadyAppliedoOffers(Guid userId)
        {
            var offers = from offer in _context.Offers
                         join application in _context.Applicationss on offer.Id equals application.OfferId
                         where application.CreatedById == userId
                         select offer;

            return offers.ToList();
        }

        public Offers GetOfferById(Guid id)
        {
            return _context.Offers
                    .Include(x=>x.ContractType)
                    .Where(x => x.Id == id)
                    .First();
        }

        public async Task SendNorificationToHRMember(string email, string userName)
        {
            var section = _configuration.GetSection("SendGrid");
            string apiKey = section.GetValue<string>("SendGridKey");
            string senderMail = section.GetValue<string>("Email");
            string senderName = section.GetValue<string>("Name");

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderMail, senderName);
            List<EmailAddress> tos = new List<EmailAddress>
            {
                new EmailAddress(email, userName)
            };

            var subject = "Nowa aplikacja";
            var htmlContent = "<strong>Na stronie HRApplication dodano nową aplikację na Twoje stanowisko</strong><br/>Zaloguj się tam, aby sprawdzić nowe zgłoszenie i na nie odpowiedzieć";
            var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
