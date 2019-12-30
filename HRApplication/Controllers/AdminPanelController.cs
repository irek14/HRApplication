﻿using System;
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

        [HttpGet]
        public PagingViewModel GetApplications(int pageSize, int pageNumber, DateTime? dateSince, DateTime? dateTo, string jobOffer, string person)
        {

            List<TableApplicationViewModel> applications = _adminService.GetAllApplications(dateSince, dateTo, jobOffer, person);
            PagingViewModel result = new PagingViewModel();

            result.Applications = applications.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            result.TotalRecord = applications.Count();
            result.TotalPage = (result.TotalRecord / pageSize) + ((result.TotalRecord % pageSize) > 0 ? 1 : 0);

            return result;
        }

        [HttpGet]
        public IActionResult ChangeRole()
        {
            return View(_adminService.GetAllUsersWithUserRole());
        }

        [HttpPost]
        public IActionResult ChangeRole(List<Guid> userIds)
        {
            _adminService.ChangeRoles(userIds);
            return Ok();
        }
    }
}