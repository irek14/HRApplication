using HRApplication.BuisnessEntities.Enums;
using HRApplication.BusinessLogic.Models.JobOffer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.BusinessLogic.Interfaces
{
    public interface IJobOfferService
    {
        List<SelectListItem> GetContractTypes();

        Task CreateJobOffer(string title, string description, Guid contractType,string salaryFrom, string salaryTo, bool partTime, decimal? weekHours, string position, DateTime? endDate);

        List<TableJobOfferViewModel> GetAllMyOffers(Guid hrMemberId);

        void DeleteJobOffer(Guid offerId);
    }
}
