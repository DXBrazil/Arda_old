using System;

namespace Arda.Common.ViewModels.Main
{
    public class WorkloadsByUserViewModel
    {
        public Guid _WorkloadID { get; set; }

        public string _WorkloadTitle { get; set; }

        public DateTime _WorkloadStartDate { get; set; }

        public DateTime _WorkloadEndDate { get; set; }

        public int _WorkloadStatus { get; set; }
        
    }
}
