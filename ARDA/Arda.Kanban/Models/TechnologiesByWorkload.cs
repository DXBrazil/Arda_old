using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    [Table("TechnologiesByWorkloads")]
    public class TechnologiesByWorkload
    {
        public Guid TechnologyID { get; set; }

        public Technology Technology { get; set; }

        public Guid WorkloadID { get; set; }

        public Workload Workload { get; set; }
    }
}
