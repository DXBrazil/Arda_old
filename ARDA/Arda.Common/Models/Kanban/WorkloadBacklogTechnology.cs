using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.Kanban.Models
{
    [Table("WorkloadBacklogTechnologies")]
    public class WorkloadBacklogTechnology
    {
        [Key]
        [Required]
        public Guid WBUTechnologyID { get; set; }

        public virtual WorkloadBacklog WorkloadBacklog { get; set; }

        public virtual Technology Technology { get; set; }
    }
}
