using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.Models
{
   [Table("UsersProperties")]
    public class UserProperties
    {
        [Key]
        public string UniqueName { get; set; }
        public string AuthCode { get; set; }
        [Required]
        public List<UserProperties> Permissions { get; set; }
    }
}

