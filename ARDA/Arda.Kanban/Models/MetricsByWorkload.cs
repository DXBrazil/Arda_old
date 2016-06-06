using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    [Table("MetricsByWorkloads")]
    public class MetricsByWorkload
    {
        // n to n join table
        public Guid MetricID { get; set; }

        public Metric Metric { get; set; }

        public Guid WorkloadID { get; set; }

        public Workload Workload { get; set; }
    }
}
