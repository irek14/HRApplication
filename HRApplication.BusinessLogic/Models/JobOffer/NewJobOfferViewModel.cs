using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.WWW.Models.JobOffer
{
    public class NewJobOfferViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Proszę podać tytuł"), MaxLength(255, ErrorMessage = "Tytuł jest zbyt długi")]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Proszę wprowadzić opis"),MinLength(50, ErrorMessage = "Opis jest zbyt krótki"), MaxLength(1000, ErrorMessage = "Opis jest zbyt długi")]
        public string Description { get; set; }

        [Display(Name = "Typ umowy")]
        [Required]
        public Guid ContractTypeId { get; set; }

        [Display(Name = "Pensja od")]
        [RegularExpression(@"[0-9]+\.*[0-9]*", ErrorMessage = "Wprowadź poprawną liczbę")]
        public string SalaryFrom { get; set; }

        [Display(Name = "Pensja do")]
        [RegularExpression(@"[0-9]+\.*[0-9]*", ErrorMessage = "Wprowadź poprawną liczbę")]
        public string SalaryTo { get; set; }

        [Display(Name = "Część etatu")]
        public bool PartTimeWork { get; set; }

        [Display(Name = "Godziny tygodniowo")]
        [RegularExpression(@"[0-9]+\.*[0-9]*", ErrorMessage = "Wprowadź poprawną liczbę")]
        public decimal HoursPerWeek { get; set; }

        [Display(Name = "Data zakończenia ogłoszenia")]
        public DateTime? EndDate { get; set; }

        [MaxLength(255, ErrorMessage = "Nazwa jest zbyt długa")]
        [Display(Name = "Stanowisko")]
        public string Position { get; set; }
    }
}
