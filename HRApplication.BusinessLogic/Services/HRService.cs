using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.HR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HRApplication.BusinessLogic.Models.HR;

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
                              Title = jobOffer.Title
                          }).FirstOrDefault();

            return result;
        }
    }
}
