using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.WWW.Models.AdminPanel
{
    public class TableApplicationViewModel
    {
        public Guid ApplicationId { get; set; }
        public string JobOfferTitle { get; set; }
        public string ApplicationState { get; set; }
        public string UserName { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
