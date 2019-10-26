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

namespace HRApplication.WWW.Controllers
{
    public class JobOfferController : Controller
    {
        private readonly IJobOfferService _jobOfferService;

        public JobOfferController(IJobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }

        // GET: JobOffer
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: JobOffer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ContractTypeId,PartTimeWork,HoursPerWeek,CreatedOn,EndDate,Position")] JobOfferViewModel offers)
        {
            if (ModelState.IsValid)
            {

            }
            return View(offers);
        }

        // GET: JobOffer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: JobOffer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,ContractTypeId,PartTimeWork,HoursPerWeek,CreatedOn,EndDate,Position,CreatedById")] Offers offers)
        {
            return View();
        }

        // GET: JobOffer/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            return View();
        }

        // POST: JobOffer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            return View();
        }

        private bool OffersExists(Guid id)
        {
            return true;
        }
    }
}
