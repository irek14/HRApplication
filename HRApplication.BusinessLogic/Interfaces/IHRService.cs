using HRApplication.BusinessLogic.Models.HR;
using HRApplication.WWW.Models.HR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.BusinessLogic.Interfaces
{
    public interface IHRService
    {
        List<TableApplicationViewModel> GetAllApplications(Guid HRMemberId, DateTime? dateSince, DateTime? dateTo, string jobOffer, string person);

        ApplicationDetailsViewModel GetApplicationDetails(Guid appId);

        Task<byte[]> DownloadCV(string CVFileName);

        void RejectApplication(Guid applicationId, Guid userId);

        void ApproveApplication(Guid applicationId, Guid userId);
    }
}
