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
            string connectionString = _configuration.GetConnectionString("AzureBlob");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            string containerName = "cvfiles";
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

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
                        CvfileName = CV.FileName,
                        Id = Guid.NewGuid(),
                        OfferId = JobOfferId,
                        ApplicationStatusHistoryId = statusHistory.Id,
                        CurrentApplicationStateName = ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.New].name
                    };

                    statusHistory.ApplicationStateId = ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.New].id;
                    statusHistory.ApplicationId = application.Id;
                    statusHistory.Date = application.CreateOn;

                    _context.Applicationss.Add(application);
                    _context.ApplicationStatusHistory.Add(statusHistory);

                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckIsOfferIsAlreadyApplied(Guid userId, Guid jobOfferId)
        {
            var offers = from offer in _context.Offers
                         join application in _context.Applicationss on offer.Id equals application.OfferId
                         where application.CreatedById == userId && offer.Id == jobOfferId
                         select offer;

            return offers.Count() != 0;
        }

        public List<Offers> GetAllJobOffers()
        {
            return _context.Offers
                    .Include(x=>x.ContractType)
                    .Where(x => x.EndDate >= DateTime.Now)
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
    }
}
