using Arda.Common.Kanban.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.ViewModels
{
    [Table("UsersKanban")]
    public class UserKanbanViewModel
    {
        [Key]
        [Required]
        public string UniqueName { get; set; }


        public virtual IEnumerable<WorkloadBacklogUser> WBUsers { get; set; }

        public virtual IEnumerable<Appointment> AppointmentUsers { get; set; }
    }
}
