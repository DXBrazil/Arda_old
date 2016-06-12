using Arda.Common.ViewModels.Kanban;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Arda.Common.Models.Kanban
{
    [Table("WorkloadBacklogUsers")]
    public class WorkloadBacklogUser
    {
        [Key]
        [Required]
        public Guid WBUserID { get; set; }

        public virtual WorkloadBacklog WorkloadBacklog { get; set; }

        public virtual UserKanbanViewModel KanbanUser { get; set; }
    }
}
