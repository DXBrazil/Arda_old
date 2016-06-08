using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.Kanban.Models
{
    [Table("WorkloadBacklogMetrics")]
    public class WorkloadBacklogMetric
    {
        [Key]
        [Required]
        public Guid WBMetricID { get; set; }

        public virtual WorkloadBacklog WorkloadBacklog { get; set; }

        public virtual Metric Metric { get; set; }
    }
}
