using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.Models
{
    public class UserPermissionsContext : DbContext
    {
        public DbSet<UsersPermissions> UsersPermissions { get; set; }
    }
}
