using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.JobOffer;
using HRApplication.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HRApplication.WWW.Controllers
{
    [Authorize(Roles = "HR")]
    public class JobOfferController : Controller
    {
        private readonly IJobOfferService _jobOfferService;

        public JobOfferController(IJobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }

        // GET: JobOffer
        public IActionResult Index()
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return View(_jobOfferService.GetAllMyOffers(userId));
        }

        // GET: JobOffer/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            return View(_jobOfferService.GetJobOfferWithId(id));
        }

        // GET: JobOffer/Create
        public IActionResult Create()
        {
            ViewData["ContractTypes"] = _jobOfferService.GetContractTypes();

            return View();
        }

        // POST: JobOffer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("JobOffer/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewJobOfferViewModel offers)
        {
            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                await _jobOfferService.CreateJobOffer(offers.Title, offers.Description, offers.ContractTypeId,offers.SalaryFrom, offers.SalaryTo, offers.PartTimeWork, offers.HoursPerWeek, offers.Position, offers.EndDate, userId);

                return RedirectToAction("Index", "JobOffer");
            }

            ViewData["ContractTypes"] = _jobOfferService.GetContractTypes();

            return View(offers);
        }

        // GET: JobOffer/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewData["ContractTypes"] = _jobOfferService.GetContractTypes();

            return View(_jobOfferService.GetJobOfferToEdit(id));
        }

        // POST: JobOffer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("JobOffer/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewJobOfferViewModel jobOffer)
        {
            _jobOfferService.EditJobOffer(jobOffer);

            return RedirectToAction("Index", "JobOffer");
        }

        // GET: JobOffer/Delete/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Delete(Guid id)
        {
            _jobOfferService.DeleteJobOffer(id);


            return RedirectToAction("Index", "JobOffer");
        }
    }
}
