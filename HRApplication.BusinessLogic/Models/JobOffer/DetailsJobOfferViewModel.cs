using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRApplication.BusinessLogic.Models.JobOffer
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
        [Display(Name = "Część etatu")]
        public bool PartTimeWork { get; set; }
        [Display(Name = "Liczba godzin tygodniowo")]
        public decimal? HoursPerWeek { get; set; }
        [Display(Name = "Wprowadzona")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Data zakończenia ogłoszenia")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Pozycja")]
        public string Position { get; set; }
        [Display(Name = "Stworzona przez")]
        public string CreatedBy { get; set; }
        [Display(Name = "Pensja")]
        public string Salary{ get; set; }
        [Display(Name = "Czy usunięta")]
        public bool IsArchived { get; set; }
    }
}
