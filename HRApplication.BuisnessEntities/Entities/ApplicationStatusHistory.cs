using System;
using System.Collections.Generic;

namespace HRApplication.DataAccess.Entities
{
    public partial class ApplicationStatusHistory
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid ApplicationStateId { get; set; }
        public DateTime Date { get; set; }

        public virtual Applicationss Application { get; set; }
        public virtual ApplicationStates ApplicationState { get; set; }
    }
}
