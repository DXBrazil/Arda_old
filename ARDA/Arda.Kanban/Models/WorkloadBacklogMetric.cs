using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    public class WorkloadBacklogMetric
    {
        public Guid WBMetricID { get; set; }

        public virtual WorkloadBacklog WorkloadBacklog { get; set; }

        public virtual Metric Metric { get; set; }
    }
}
