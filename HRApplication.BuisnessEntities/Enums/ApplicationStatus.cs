using System;
using System.Collections.Generic;
using System.Text;

namespace HRApplication.BuisnessEntities.Enums
{
    public enum ApplicationStatus
    {
        Approved = 0,
        Rejected = 1,
        New = 2
    }

    public class ApplicationStatusesData
    {
        public static (Guid id, string name)[] applicationStatusesIds = {(Guid.Parse("6FA4DF7C-F461-43AE-990C-14DBDF41C5AC"),"Approved"),
                                                        (Guid.Parse("14C96728-6B74-4E09-B251-33CA3232CD02"),"Rejected"),
                                                        (Guid.Parse("80A4DC49-2F91-411E-81BC-9486318D24C8"),"New")};
    }



}
