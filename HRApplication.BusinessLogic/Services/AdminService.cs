using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HRApplication.WWW.Models.AdminPanel;

namespace HRApplication.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
        private readonly HRAppDBContext _context;
        private IConfiguration _configuration;

        public AdminService(HRAppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<TableApplicationViewModel> GetAllApplications(DateTime? dateSince, DateTime? dateTo, string jobOffer, string person)
        {
            if (dateTo != null)
                dateTo = dateTo.Value.AddDays(1);

            var result = from app in _context.Applicationss
                         where (dateSince == null || app.CreateOn >=dateSince) && (dateTo == null || app.CreateOn <= dateTo)
                         join aplicant in _context.Users on app.CreatedById equals aplicant.Id
                         join offer in _context.Offers on app.OfferId equals offer.Id
                         where jobOffer == null || offer.Title.Contains(jobOffer)
                         join hrWorker in _context.Users on offer.CreatedById equals hrWorker.Id
                         select new TableApplicationViewModel()
                         {
                             ApplicationId = app.Id,
                             ApplicationDate = app.CreateOn,
                             ApplicationState = app.CurrentApplicationStateName,
                             HRMemberName = app.Offer.CreatedBy.FirstName + " " + app.Offer.CreatedBy.LastName,
                             JobOfferTitle = app.Offer.Title,
                             UserName = app.CreatedBy.FirstName + " " + app.CreatedBy.LastName
                         };

            if (person != null)
                result = result.Where(x => x.UserName.Contains(person));

            return result.ToList();
        }
    }
}
