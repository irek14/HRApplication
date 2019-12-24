using System;
using System.Collections.Generic;

namespace HRApplication.DataAccess.Entities
{
    public partial class Users
    {
        public Users()
        {
            Applications = new HashSet<Applications>();
            Offers = new HashSet<Offers>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }

        public virtual UserRoles Role { get; set; }
        public virtual ICollection<Applications> Applications { get; set; }
        public virtual ICollection<Offers> Offers { get; set; }
        public virtual ICollection<ApplicationStatusHistory> StatusHistories { get; set; }
    }
}
