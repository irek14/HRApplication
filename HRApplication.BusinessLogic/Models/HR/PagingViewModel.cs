using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.WWW.Models.HR
{
    public class PagingViewModel
    {
        public List<TableApplicationViewModel> Applications { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
    }
}
