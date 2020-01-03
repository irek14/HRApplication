using HRApplication.BuisnessEntities.Enums;
using HRApplication.BusinessLogic.Models.JobOffer;
using HRApplication.WWW.Models.JobOffer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DetailsJobOfferViewModel = HRApplication.BusinessLogic.Models.JobOffer.DetailsJobOfferViewModel;

namespace HRApplication.BusinessLogic.Interfaces
{
    public interface IJobOfferService
    {
        List<SelectListItem> GetContractTypes();

        Task CreateJobOffer(string title, string description, Guid contractType,string salaryFrom, string salaryTo, bool partTime, decimal? weekHours, string position, DateTime? endDate);

        List<TableJobOfferViewModel> GetAllMyOffers(Guid hrMemberId);

        void DeleteJobOffer(Guid offerId);

        NewJobOfferViewModel GetJobOfferToEdit(Guid offerId);

        void EditJobOffer(NewJobOfferViewModel offer);

        DetailsJobOfferViewModel GetJobOfferWithId(Guid id);
    }
}
