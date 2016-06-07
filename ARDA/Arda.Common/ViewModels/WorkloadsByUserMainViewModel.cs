using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.ViewModels
{
    public class WorkloadsByUserMainViewModel
    {
        public Guid _WorkloadID { get; set; }

        public string _WorkloadTitle { get; set; }

        public DateTime _WorkloadStartDate { get; set; }

        public DateTime _WorkloadEndDate { get; set; }
    }
}
