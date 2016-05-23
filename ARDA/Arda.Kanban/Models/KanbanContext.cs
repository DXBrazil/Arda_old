using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Models
{
    public class KanbanContext : DbContext
    {
        public DbSet<FiscalYear> FiscalYears { get; set; }
    }
}
