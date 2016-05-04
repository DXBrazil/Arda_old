using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Authentication.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserID { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The field '{0}' accept only {1} characters at max.")]
        [MinLength(3, ErrorMessage = "The field '{0}' must have {1} characters or more.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please, inform a valid email.")]
        public string Email { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The password '{0}' accept only {1} characters at max.")]
        [MinLength(3, ErrorMessage = "The password '{0}' must have {1} characters or more.")]
        public string Password { get; set; }

        public string Avatar { get; set; }
    }
}
