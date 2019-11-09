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
    }
}
