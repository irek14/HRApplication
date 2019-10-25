using System;
using System.Collections.Generic;

namespace HRApplication.DataAccess.Entities
{
    public partial class Users
    {
        public Users()
        {
            Applicationss = new HashSet<Applicationss>();
            Offers = new HashSet<Offers>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }

        public virtual UserRoles Role { get; set; }
        public virtual ICollection<Applicationss> Applicationss { get; set; }
        public virtual ICollection<Offers> Offers { get; set; }
    }
}
