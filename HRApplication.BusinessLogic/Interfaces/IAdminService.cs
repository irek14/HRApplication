using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Models.AdminPanel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRApplication.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        List<TableApplicationViewModel> GetAllApplications(DateTime? dateSince, DateTime? dateTo, string jobOffer, string person);
    }
}
