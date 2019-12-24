using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRApplication.BusinessLogic.Models.JobOffer
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
    }
}
