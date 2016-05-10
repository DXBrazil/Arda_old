using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.Models
{
    public class PermissionsContext : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
    }
}
