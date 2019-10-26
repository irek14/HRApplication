using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.WWW.Models.JobOffer
{
    public class JobOfferViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Typ umowy")]
        public Guid ContractTypeId { get; set; }

        [Display(Name = "Pensja od")]
        public int SalaryFrom { get; set; }

        [Display(Name = "Pensja do")]
        public int SalaryTo { get; set; }

        [Display(Name = "Część etatu")]
        public bool PartTimeWork { get; set; }

        [Display(Name = "Godziny tygodniowo")]
        public decimal HoursPerWeek { get; set; }

        [Display(Name = "Data zakończenia ogłoszenia")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Stanowisko")]
        public string Position { get; set; }
    }
}
