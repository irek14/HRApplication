using HRApplication.BuisnessEntities.Enums;
using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRApplication.BusinessLogic.Services
{
    public class JobOfferService : IJobOfferService
    {
        private readonly HRAppDBContext _context;

        public JobOfferService(HRAppDBContext context)
        {
            _context = context;
        }

        public Task CreateJobOffer(string title, string description, ContractType contractType, bool partTime = false, decimal weekHours = 40, string position = "", DateTime? endDate = null)
        {
            throw new NotImplementedException();
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
