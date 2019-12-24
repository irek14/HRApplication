using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.HR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HRApplication.BusinessLogic.Models.HR;
using System.IO;
using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using HRApplication.BuisnessEntities.Enums;

namespace HRApplication.BusinessLogic.Services
{
    public class HRService : IHRService
    {
        private readonly HRAppDBContext _context;
        private IConfiguration _configuration;

        public HRService(HRAppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<byte[]> DownloadCV(string CVFileName)
        {
            var section = _configuration.GetSection("Azure");
            string containerName = section.GetValue<string>("ContainerName");
            string _storageConnection = _configuration.GetConnectionString("AzureBlob");
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConnection);
            CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(CVFileName);
            MemoryStream memStream = new MemoryStream();

            await _blockBlob.DownloadToStreamAsync(memStream);

            var byteArray = new byte[memStream.Length];
            memStream.Position = 0;
            memStream.Read(byteArray, 0, (int)memStream.Length);

            return byteArray;
        }

        public List<TableApplicationViewModel> GetAllApplications(Guid HRMemberId,DateTime? dateSince, DateTime? dateTo, string jobOffer, string person)
        {
            if (dateTo != null)
                dateTo = dateTo.Value.AddDays(1);

            var result = from app in _context.Applicationss
                         where (dateSince == null || app.CreateOn >= dateSince) && (dateTo == null || app.CreateOn <= dateTo)
                         join aplicant in _context.Users on app.CreatedById equals aplicant.Id
                         join offer in _context.Offers on app.OfferId equals offer.Id
                         where jobOffer == null || offer.Title.Contains(jobOffer)
                         join hrWorker in _context.Users on offer.CreatedById equals hrWorker.Id
                         where hrWorker.Id == HRMemberId
                         select new TableApplicationViewModel()
                         {
                             ApplicationId = app.Id,
                             ApplicationDate = app.CreateOn,
                             ApplicationState = app.CurrentApplicationStateName,
                             JobOfferTitle = app.Offer.Title,
                             UserName = app.CreatedBy.FirstName + " " + app.CreatedBy.LastName
                         };

            if (person != null)
                result = result.Where(x => x.UserName.Contains(person));

            return result.ToList();
        }

        public ApplicationDetailsViewModel GetApplicationDetails(Guid appId)
        {
            var result = (from app in _context.Applicationss
                          where app.Id == appId
                          join jobOffer in _context.Offers on app.OfferId equals jobOffer.Id
                          join user in _context.Users on app.CreatedById equals user.Id
                          select new ApplicationDetailsViewModel()
                          {
                              Id = app.Id,
                              CreatedBy = user.FirstName + " " + user.LastName,
                              CreateOn = app.CreateOn,
                              CurrentApplicationStateName = app.CurrentApplicationStateName,
                              CvfileName = app.CvfileName,
                              OfferId = jobOffer.Id,
                              Position = jobOffer.Position,
                              Title = jobOffer.Title,
                              IsNew = app.CurrentApplicationStateName == ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.New].name
                          }).FirstOrDefault();

            return result;
        }

        public void RejectApplication(Guid applicationId)
        {
            Applications app = (from application in _context.Applicationss
                                where application.Id == applicationId
                                select application).FirstOrDefault();

            if (app == null)
                return;

            app.CurrentApplicationStateName = ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.Rejected].name;

            ApplicationStatusHistory state = new ApplicationStatusHistory
            {
                Id = Guid.NewGuid(),
                ApplicationStateId = ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.Rejected].id,
                ApplicationId = app.Id,
                Date = DateTime.Now,
                UserId = Guid.Parse("17496B8A-8E4E-4E8A-8099-101998018B03") //TODO: Change after add Identity
            };

            _context.ApplicationStatusHistory.Add(state);

            _context.SaveChanges();
        }

        public void ApproveApplication(Guid applicationId)
        {
            Applications app = (from application in _context.Applicationss
                                where application.Id == applicationId
                                select application).FirstOrDefault();

            if (app == null)
                return;

            app.CurrentApplicationStateName = ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.Approved].name;

            ApplicationStatusHistory state = new ApplicationStatusHistory
            {
                Id = Guid.NewGuid(),
                ApplicationStateId = ApplicationStatusesData.applicationStatusesIds[(int)ApplicationStatus.Approved].id,
                ApplicationId = app.Id,
                Date = DateTime.Now,
                UserId = Guid.Parse("17496B8A-8E4E-4E8A-8099-101998018B03") //TODO: Change after add Identity
            };

            _context.ApplicationStatusHistory.Add(state);

            _context.SaveChanges();
        }
    }
}
