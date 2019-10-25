using System;
using System.Collections.Generic;

namespace HRApplication.DataAccess.Entities
{
    public partial class Offers
    {
        public Offers()
        {
            Applicationss = new HashSet<Applicationss>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ContractTypeId { get; set; }
        public bool PartTimeWork { get; set; }
        public decimal? HoursPerWeek { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? EndDate { get; set; }
        public string Position { get; set; }
        public Guid? CreatedById { get; set; }

        public virtual ContractTypes ContractType { get; set; }
        public virtual Users CreatedBy { get; set; }
        public virtual ICollection<Applicationss> Applicationss { get; set; }
    }
}
