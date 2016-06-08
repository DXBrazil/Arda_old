using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.Models.Kanban
{
    [Table("Metrics")]
    public class Metric
    {
        [Key]
        [Required]
        public Guid MetricID { get; set; }

        [Required]
        public string MetricCategory { get; set; }

        [Required]
        public string MetricName { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual FiscalYear FiscalYear { get; set; }

        public virtual IEnumerable<WorkloadBacklogMetric> WBMetrics { get; set; }
    }
}
