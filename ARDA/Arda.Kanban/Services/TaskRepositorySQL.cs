using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Arda.Kanban
{
    class TaskRepositorySQL : ITaskRepository
    {
        TaskDbContext _context;

        public TaskRepositorySQL(TaskDbContext context)
        {
            _context = context;            
        }

        // Need to fix the primary key generation
        private int _newId = 100;

        string NewId()
        {
            return "d" + (_newId++) + ":" + DateTime.Now;
        }

        public TaskItem Add(TaskItem item)
        {
            string id = item.Id = NewId();
                        
            var saved = _context.Add(item);
            _context.SaveChanges();

            return saved.Entity;
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _context.Tasks;
        }

        public TaskItem GetById(string id)
        {
            return _context.Tasks.Where(t => t.Id == id).First();
        }

        public bool Remove(string id)
        {
            _context.Remove(new TaskItem() { Id = id });
            _context.SaveChanges();
            return true;
        }

        public void Update(TaskItem item)
        {
            var initial = GetById(item.Id);

            if (item.Name != null)   initial.Name = item.Name;
            if (item.Description != null)  initial.Description = item.Description;

            initial.State = item.State;

            _context.SaveChanges();
        }
    }

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
