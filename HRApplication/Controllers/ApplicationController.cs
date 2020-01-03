using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            List<Offers> alreadyApplied = _applicationService.GetAlreadyAppliedoOffers(userId);
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

        [HttpGet("Application/Details")]
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
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

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
                IsAlreadyApplied = _applicationService.CheckIsOfferIsAlreadyApplied(userId, offer.Id),
                IsApproved = _applicationService.CheckIfOfferIsApproved(userId, offer.Id),
                IsNew = _applicationService.CheckIfOfferIsNew(userId, offer.Id)
            };

            return View(model);
        }

        /// <summary>
        /// Function add CV to a job offer with specified Id
        /// </summary>
        /// <param name="JobOfferId">Id of the job offer</param>
        /// <param name="file">CV file in .pdf format</param>
        /// <returns>Redirect to Index action</returns>
        //[HttpPost("Application/Details/Add")]
        public async Task<IActionResult> Add(Guid JobOfferId, IFormFile file)
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            bool result = await _applicationService.AddNewApplication(JobOfferId, file, userId);

            if (!result)
                return BadRequest();

            return RedirectToAction("Index", "Application");
        }

        [HttpPost("Application/Details/Delete")]
        public async Task<IActionResult> Delete(Guid JobOfferId)
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            await _applicationService.DeleteApplication(JobOfferId, userId);

            return Json(new { redirecturl = Url.Action("Index","Application") });
        }

        [HttpPost("Application/Details/Edit")]
        public IActionResult Edit(Guid JobOfferId, IFormFile file)
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _applicationService.EditApplication(JobOfferId, file, userId);

            return Ok();
        }
    }
}