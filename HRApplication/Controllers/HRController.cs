using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.HR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRApplication.WWW.Controllers
{
    [Authorize(Roles = "HR")]
    public class HRController : Controller
    {
        IHRService _hrService;

        public HRController(IHRService hrService)
        {
            _hrService = hrService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("hr/GetApplications")]
        public PagingViewModel GetApplications(int pageSize, int pageNumber, DateTime? dateSince, DateTime? dateTo, string jobOffer, string person)
        {
            Guid hrMemberId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

            List<TableApplicationViewModel> applications = _hrService.GetAllApplications(hrMemberId,dateSince, dateTo, jobOffer, person);
            PagingViewModel result = new PagingViewModel();

            result.Applications = applications.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            result.TotalRecord = applications.Count();
            result.TotalPage = (result.TotalRecord / pageSize) + ((result.TotalRecord % pageSize) > 0 ? 1 : 0);

            return result;
        }

        [HttpGet]
        public IActionResult Details(Guid Id)
        {
            return View(_hrService.GetApplicationDetails(Id));
        }

        [HttpGet("hr/DownloadCV")]
        public async Task<IActionResult> DownloadCV(string CVFileName)
        {
            byte[] array = await _hrService.DownloadCV(CVFileName);

            return File(array, System.Net.Mime.MediaTypeNames.Application.Pdf, CVFileName + ".pdf");
        }

        [HttpGet("hr/Accept")]
        public IActionResult Accept(Guid id)
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _hrService.ApproveApplication(id, userId);

            return RedirectToAction("Index", "hr");
        }

        [HttpGet("hr/Reject")]
        public IActionResult Reject(Guid id)
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _hrService.RejectApplication(id, userId);

            return RedirectToAction("Index", "hr");
        }
    }
}
