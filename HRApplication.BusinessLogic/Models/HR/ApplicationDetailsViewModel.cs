using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRApplication.BusinessLogic.Models.HR
{
    public class ApplicationDetailsViewModel
    {
        public Guid Id { get; set; }
        [Display(Name ="Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Pozycja")]
        public string Position { get; set; }
        [Display(Name = "Wprowadzona")]
        public DateTime CreateOn { get; set; }
        [Display(Name = "Złożona przez")]
        public string CreatedBy { get; set; }
        public string CvfileName { get; set; }
        public Guid OfferId { get; set; }
        [Display(Name = "Obecny status")]
        public string CurrentApplicationStateName { get; set; }
        public bool IsNew { get; set; }
    }
}
