using HRApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRApplication.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        List<Applications> GetAllApplications(int pageSize, int pageNumber);
    }
}
