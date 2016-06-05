using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    public class WorkloadBacklogTechnology
    {
        public Guid WBUTechnologyID { get; set; }

        public virtual WorkloadBacklog WorkloadBacklog { get; set; }

        public Technology Technology { get; set; }
    }
}
