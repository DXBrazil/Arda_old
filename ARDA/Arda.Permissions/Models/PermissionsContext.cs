using Arda.Common.Models.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    public class PermissionsContext : DbContext
    {
        public DbSet<Module> Modules { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UsersPermission> UsersPermissions { get; set; }

    }
}
