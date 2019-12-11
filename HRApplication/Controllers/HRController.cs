using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.HR;
using Microsoft.AspNetCore.Mvc;

namespace HRApplication.WWW.Controllers
{
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

        [HttpGet]
        public PagingViewModel GetApplications(int pageSize, int pageNumber, DateTime? dateSince, DateTime? dateTo, string jobOffer, string person)
        {
            //TODO: Get ID from claims
            Guid hrMemberId = Guid.Parse("DACB7B3D-780B-44E8-9F68-7F62200DEAE3");

            List<TableApplicationViewModel> applications = _hrService.GetAllApplications(hrMemberId,dateSince, dateTo, jobOffer, person);
            PagingViewModel result = new PagingViewModel();

            result.Applications = applications.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            result.TotalRecord = applications.Count();
            result.TotalPage = (result.TotalRecord / pageSize) + ((result.TotalRecord % pageSize) > 0 ? 1 : 0);

            return result;
        }
    }
}
