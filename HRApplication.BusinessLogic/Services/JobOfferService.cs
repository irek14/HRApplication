using HRApplication.BuisnessEntities.Enums;
using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRApplication.BusinessLogic.Models.JobOffer;

namespace HRApplication.BusinessLogic.Services
{
    public class JobOfferService : IJobOfferService
    {
        private readonly HRAppDBContext _context;

        public JobOfferService(HRAppDBContext context)
        {
            _context = context;
        }

        public async Task CreateJobOffer(string title, string description, Guid contractType, string salaryFrom, string salaryTo, bool partTime, decimal? weekHours, string position, DateTime? endDate)
        {
            Offers offer = new Offers {
                Id = Guid.NewGuid(),
                ContractTypeId = contractType,
                CreatedById = Guid.Parse("DACB7B3D-780B-44E8-9F68-7F62200DEAE3"),
                CreatedOn = DateTime.Now,
                Description = description,
                HoursPerWeek = weekHours,
                EndDate = endDate,
                Position = position,
                PartTimeWork = partTime,
                Title = title,
                SalaryFrom = salaryFrom == null ?(int?)null : int.Parse(salaryFrom),
                SalaryTo = salaryTo == null ? (int?)null : int.Parse(salaryTo)
            };

            _context.Offers.Add(offer);

            await _context.SaveChangesAsync();
        }

        public List<TableJobOfferViewModel> GetAllMyOffers(Guid hrMemberId)
        {
            return (from offer in _context.Offers
                   where offer.CreatedById == hrMemberId
                   select new TableJobOfferViewModel()
                   {
                       Id = offer.Id,
                       ContractType = offer.ContractType.ContractTypeName,
                       Position = offer.Position,
                       Salary = offer.SalaryFrom + "-" + offer.SalaryTo,
                       Title = offer.Title
                   }).ToList();
        }

        public List<SelectListItem> GetContractTypes()
        {
            var contractTypes = from type in _context.ContractTypes
                         select new { typeId = type.Id.ToString(),typeName = type.ContractTypeName };

            List<SelectListItem> result = new List<SelectListItem>();

            foreach(var type in contractTypes)
            {
                result.Add(new SelectListItem(type.typeName,type.typeId));
                
            }

            return result;
        }
    }
}
