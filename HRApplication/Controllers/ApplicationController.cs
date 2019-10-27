using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.Application;
using Microsoft.AspNetCore.Mvc;

namespace HRApplication.WWW.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public IActionResult Index()
        {
            List<Offers> offers = _applicationService.GetAllJobOffers();
            List<TableJobOfferViewModel> model = new List<TableJobOfferViewModel>();

            foreach(var offer in offers)
            {
                model.Add(new TableJobOfferViewModel()
                {
                    Id = offer.Id,
                    ContractType = offer.ContractType.ContractTypeName,
                    Position = offer.Position,
                    Salary = offer.SalaryFrom + "-" + offer.SalaryTo,
                    Title = offer.Title
                });
            }

            return View(model);
        }
    }
}