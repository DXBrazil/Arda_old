using Arda.Common.Models.Kanban;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.ViewModels.Kanban
{
    [Table("UsersKanban")]
    public class UserKanbanViewModel
    {
        [Key]
        [Required]
        public string UniqueName { get; set; }

        public virtual IEnumerable<WorkloadBacklogUser> WBUsers { get; set; }

        public virtual ICollection<Appointment> AppointmentUsers { get; set; }
    }
}
