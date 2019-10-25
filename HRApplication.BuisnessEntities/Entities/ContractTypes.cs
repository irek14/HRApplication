using System;
using System.Collections.Generic;

namespace HRApplication.DataAccess.Entities
{
    public partial class ContractTypes
    {
        public ContractTypes()
        {
            Offers = new HashSet<Offers>();
        }

        public Guid Id { get; set; }
        public string ContractTypeName { get; set; }

        public virtual ICollection<Offers> Offers { get; set; }
    }
}
