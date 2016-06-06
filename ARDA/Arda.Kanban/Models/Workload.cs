using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    [Table("Workloads")]
    public class Workload
    {
        [Key]
        [Required]
        public Guid WorkloadID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EstimatedEndDate { get; set; }

        [Required]
        public string Activity { get; set; }

        [Required]
        public string Expertise { get; set; }

        [Required]
        public string Complexity { get; set; }

        [Required]
        public virtual ICollection<MetricsByWorkload> MetricsByWorkloads { get; set; }

        [Required]
        public virtual ICollection<TechnologiesByWorkload> TechnologiesByWorkloads { get; set; }

        [Required]
        public string Users { get; set; } // Will host a json string with all selected users

        public string FileOrVirtualDirectoryLink { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
