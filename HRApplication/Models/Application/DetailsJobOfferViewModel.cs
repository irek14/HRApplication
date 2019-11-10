using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.WWW.Models.JobOffer
{
    public class DetailsJobOfferViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Typ umowy")]
        public string ContractType { get; set; }

        [Display(Name = "Pensja")]
        public string Salary { get; set; }

        [Display(Name = "Część etatu")]
        public bool PartTimeWork { get; set; }

        [Display(Name = "Godziny tygodniowo")]
        public decimal? HoursPerWeek { get; set; }

        [Display(Name = "Upływa")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Stanowisko")]
        public string Position { get; set; }

        public bool IsAlreadyApplied { get; set; }
    }
}
