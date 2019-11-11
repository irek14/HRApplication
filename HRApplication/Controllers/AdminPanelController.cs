using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.BusinessLogic.Interfaces;
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
    }
}