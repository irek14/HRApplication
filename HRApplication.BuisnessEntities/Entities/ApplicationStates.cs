using System;
using System.Collections.Generic;

namespace HRApplication.DataAccess.Entities
{
    public partial class ApplicationStates
    {
        public ApplicationStates()
        {
            ApplicationStatusHistory = new HashSet<ApplicationStatusHistory>();
        }

        public Guid Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<ApplicationStatusHistory> ApplicationStatusHistory { get; set; }
    }
}
