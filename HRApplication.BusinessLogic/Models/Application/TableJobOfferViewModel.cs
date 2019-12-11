using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.WWW.Models.Application
{
    public class TableJobOfferViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Typ umowy")]
        [Required]
        public string ContractType { get; set; }

        [Display(Name = "Pensja")]
        public string Salary { get; set; }

        [Display(Name = "Stanowisko")]
        public string Position { get; set; }

        public bool IsAlreadyAppliedf { get; set; }
    }
}
