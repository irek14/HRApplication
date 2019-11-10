using HRApplication.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.BusinessLogic.Interfaces
{
    public interface IApplicationService
    {
        List<Offers> GetAllJobOffers();

        Offers GetOfferById(Guid id);

        Task<bool> AddNewApplication(Guid JobOfferId, IFormFile CV);

        List<Offers> GetAlreadyAppliedoOffers(Guid userId);

        bool CheckIsOfferIsAlreadyApplied(Guid userId, Guid jobOfferId);

        Task DeleteApplication(Guid JobOfferId);

        void EditApplication(Guid JobOfferId, IFormFile CV);
    }
}
