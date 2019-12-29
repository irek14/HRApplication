using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.Application;
using HRApplication.WWW.Models.JobOffer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRApplication.WWW.Controllers
{
    [Authorize(Roles = "User, Admin, HR")]
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
            List<Offers> alreadyApplied = _applicationService.GetAlreadyAppliedoOffers(Guid.Parse("17496B8A-8E4E-4E8A-8099-101998018B03"));
            List<TableJobOfferViewModel> model = new List<TableJobOfferViewModel>();

            foreach(var offer in offers)
            {
                model.Add(new TableJobOfferViewModel()
                {
                    Id = offer.Id,
                    ContractType = offer.ContractType.ContractTypeName,
                    Position = offer.Position,
                    Salary = offer.SalaryFrom + "-" + offer.SalaryTo,
                    Title = offer.Title,
                    IsAlreadyAppliedf = alreadyApplied.Contains(offer)
                });
            }

            return View(model);
        }

        public IActionResult Details(Guid id)
        {
            Offers offer;
            try
            {
                offer = _applicationService.GetOfferById(id);
            }
            catch(Exception e)
            {
                return NotFound();
            }

            DetailsJobOfferViewModel model = new DetailsJobOfferViewModel {
                Id = offer.Id,
                ContractType = offer.ContractType.ContractTypeName,
                Description = offer.Description,
                EndDate = offer.EndDate,
                HoursPerWeek = offer.HoursPerWeek,
                PartTimeWork = offer.PartTimeWork,
                Position = offer.Position,
                Salary = offer.SalaryFrom + "-" + offer.SalaryTo,
                Title = offer.Title,
                IsAlreadyApplied = _applicationService.CheckIsOfferIsAlreadyApplied(Guid.Parse("17496B8A-8E4E-4E8A-8099-101998018B03"), offer.Id)
            };

            return View(model);
        }

        [Route("Application/Details/Add")]
        [HttpPost]
        public async Task<IActionResult> Add(Guid JobOfferId, IFormFile file)
        {
            bool result = await _applicationService.AddNewApplication(JobOfferId, file);

            if (!result)
                return BadRequest();

            return RedirectToAction("Index", "Application");
        }

        [Route("Application/Details/Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid JobOfferId)
        {
            await _applicationService.DeleteApplication(JobOfferId);

            return Json(new { redirecturl = Url.Action("Index","Application") });
        }

        [Route("Application/Details/Edit")]
        [HttpPost]
        public IActionResult Edit(Guid JobOfferId, IFormFile file)
        {
            _applicationService.EditApplication(JobOfferId, file);

            return Ok();
        }
    }
}