using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.WWW.Models.HR
{
    public class TableApplicationViewModel
    {
        public Guid ApplicationId { get; set; }
        [Display(Name = "Tytuł")]
        public string JobOfferTitle { get; set; }
        [Display(Name = "Status aplikacji")]
        public string ApplicationState { get; set; }
        [Display(Name = "Aplikant")]
        public string UserName { get; set; }
        [Display(Name = "Ostatnia zmiana statusu")]
        public DateTime ApplicationDate { get; set; }
    }
}
