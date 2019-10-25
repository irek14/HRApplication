using System;
using System.Collections.Generic;

namespace HRApplication.DataAccess.Entities
{
    public partial class Applicationss
    {
        public Applicationss()
        {
            ApplicationStatusHistory = new HashSet<ApplicationStatusHistory>();
        }

        public Guid Id { get; set; }
        public DateTime CreateOn { get; set; }
        public Guid CreatedById { get; set; }
        public string CvfileName { get; set; }
        public Guid OfferId { get; set; }
        public Guid ApplicationStatusHistoryId { get; set; }
        public string CurrentApplicationStateName { get; set; }

        public virtual Users CreatedBy { get; set; }
        public virtual Offers Offer { get; set; }
        public virtual ICollection<ApplicationStatusHistory> ApplicationStatusHistory { get; set; }
    }
}
