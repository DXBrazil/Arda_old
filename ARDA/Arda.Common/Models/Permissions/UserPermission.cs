using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arda.Permissions.Models
{
    [Table("UsersPermissions")]
    public class UsersPermission
    {
        [Key]
        public int PermissionID { get; set; }

        [ForeignKey("UniqueName")]
        public string UniqueName { get; set; }

        [ForeignKey("ResourceID")]
        public int ResourceID { get; set; }


        public virtual Resource Resource { get; set; }

        public virtual User User { get; set; }
    }
}