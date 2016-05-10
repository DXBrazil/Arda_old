using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.Models
{
    [Table("Permissions")]
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PermissionID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public string Token { get; set; }

        public string PermissionsByUser { get; set; }
    }
}
