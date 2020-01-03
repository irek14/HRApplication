using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRApplication.BusinessLogic.Models.AdminPanel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        [Display(Name ="Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
