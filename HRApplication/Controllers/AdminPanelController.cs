using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.AdminPanel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRApplication.WWW.Controllers
{
    [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Funnctions return applications for admin applications list with paging
        /// </summary>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="dateSince">Filter start date parameter</param>
        /// <param name="dateTo">Filter end date parameter</param>
        /// <param name="jobOffer">Filter to jobOffer name (based on contatins filter)</param>
        /// <param name="person">Filter to person name (based on contatins filter)</param>
        /// <returns></returns>
        [HttpGet("adminpanel/GetApplications")]
        public PagingViewModel GetApplications(int pageSize, int pageNumber, DateTime? dateSince, DateTime? dateTo, string jobOffer, string person)
        {

            List<TableApplicationViewModel> applications = _adminService.GetAllApplications(dateSince, dateTo, jobOffer, person);
            PagingViewModel result = new PagingViewModel();

            result.Applications = applications.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            result.TotalRecord = applications.Count();
            result.TotalPage = (result.TotalRecord / pageSize) + ((result.TotalRecord % pageSize) > 0 ? 1 : 0);

            return result;
        }

        [HttpGet("adminpanel/ChangeRole")]
        public IActionResult ChangeRole()
        {
            return View(_adminService.GetAllUsersWithUserRole());
        }

        [HttpPost("adminpanel/ChangeRole")]
        public IActionResult ChangeRole(List<Guid> userIds)
        {
            _adminService.ChangeRoles(userIds);
            return Ok();
        }
    }
}