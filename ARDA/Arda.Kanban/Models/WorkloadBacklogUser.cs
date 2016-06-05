using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Arda.Kanban.Models
{
    public class WorkloadBacklogUser
    {
        public Guid WBUserID { get; set; }

        public virtual WorkloadBacklog WorkloadBacklog { get; set; }

        public UserMainViewModel User{ get; set; }
    }
}
