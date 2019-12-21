using HRApplication.BusinessLogic.Models.HR;
using HRApplication.WWW.Models.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRApplication.BusinessLogic.Interfaces
{
    public interface IHRService
    {
        List<TableApplicationViewModel> GetAllApplications(Guid HRMemberId, DateTime? dateSince, DateTime? dateTo, string jobOffer, string person);

        ApplicationDetailsViewModel GetApplicationDetails(Guid appId);
    }
}
