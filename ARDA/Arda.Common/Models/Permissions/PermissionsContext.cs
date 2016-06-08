using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace Arda.Permissions.Models
{
    public class PermissionsContext : DbContext
    {
        public DbSet<Module> Modules { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UsersPermission> UsersPermissions { get; set; }

    }
}
