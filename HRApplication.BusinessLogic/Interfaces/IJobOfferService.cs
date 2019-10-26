using HRApplication.BuisnessEntities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.BusinessLogic.Interfaces
{
    public interface IJobOfferService
    {
        List<(string, string)> GetContractTypes();

        Task CreateJobOffer(string title, string description, ContractType contractType, bool partTime = false, decimal weekHours = 40, string position = "", DateTime? endDate = null);
    }
}
