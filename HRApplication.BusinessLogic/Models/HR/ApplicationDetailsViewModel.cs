using System;
using System.Collections.Generic;
using System.Text;

namespace HRApplication.BusinessLogic.Models.HR
{
    public class ApplicationDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public DateTime CreateOn { get; set; }
        public string CreatedBy { get; set; }
        public string CvfileName { get; set; }
        public Guid OfferId { get; set; }
        public string CurrentApplicationStateName { get; set; }
    }
}
