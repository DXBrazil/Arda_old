using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.ViewModels
{
    public class UserPermissionViewModel
    {
        [Required]
        public string Module { get; set; }
        [Required]
        public bool Enabled { get; set; }
        
        public List<UserPermissionViewModel> NestedResouces { get; set; }
    }
}
