using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    [Table("Technologies")]
    public class Technology
    {
        [Key]
        [Required]
        public Guid TechnologyID { get; set; }

        [Required]
        public string TechnologyShortText { get; set; }

        public string TechnologyDescription { get; set; }

        public virtual ICollection<TechnologiesByWorkload> TechnologiesByWorkloads { get; set; }
    }
}
