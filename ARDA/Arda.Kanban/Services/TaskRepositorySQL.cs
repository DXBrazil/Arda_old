using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Arda.Kanban
{
    public class TaskDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }
    }
    
    public class TaskDatabaseSetup
    {
        public static void InitSamples(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(TaskDbContext)) as TaskDbContext;

            var database = context.Database;

            var tasks = context.Tasks;

            bool hasTasks = tasks.Any();

            if( !hasTasks )
            {
                tasks.Add(new TaskItem() { Id = "d0", Name = "Database Task Item 1" });
                tasks.Add(new TaskItem() { Id = "d1", Name = "Database Task Item 2" });
                tasks.Add(new TaskItem() { Id = "d2", Name = "Database Task Item 3" });

                context.SaveChanges();
            }
        }
    }
}
