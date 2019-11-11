using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.AdminPanel;
using Microsoft.AspNetCore.Mvc;

namespace HRApplication.WWW.Controllers
{
    public class AdminPanelController : Controller
    {
        IAdminService _adminService;

        public AdminPanelController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PagingViewModel GetApplications(int pageSize, int pageNumber)
        {
            List<Applications> applications = _adminService.GetAllApplications();
            List<TableApplicationViewModel> applicationViewModels = new List<TableApplicationViewModel>();
            PagingViewModel result = new PagingViewModel();

            foreach (Applications app in applications)
            {
                TableApplicationViewModel model = new TableApplicationViewModel
                {
                    ApplicationId = app.Id,
                    ApplicationDate = app.CreateOn,
                    ApplicationState = app.CurrentApplicationStateName,
                    HRMemberName = app.Offer.CreatedBy.FirstName + " " + app.Offer.CreatedBy.LastName,
                    JobOfferTitle = app.Offer.Title,
                    UserName = app.CreatedBy.FirstName + " " + app.CreatedBy.LastName
                };
                applicationViewModels.Add(model);
            }

            result.Applications = applicationViewModels.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            result.TotalRecord = applications.Count();
            result.TotalPage = (result.TotalRecord / pageSize) + ((result.TotalRecord % pageSize) > 0 ? 1 : 0);

            return result;
        }

    }
}