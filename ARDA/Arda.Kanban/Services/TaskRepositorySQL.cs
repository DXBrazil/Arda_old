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

        private int _newId = 100;

        string NewId()
        {
            return "d" + (_newId++);
        }

        public void Add(TaskItem item)
        {
            string id = item.Id = NewId();

            //_tasksDict.Add(id, item);
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _context.Tasks;
        }

        public TaskItem GetById(string id)
        {
            //if (!_tasksDict.ContainsKey(id))
                return null;

            //return _tasksDict[id];
        }

        public bool Remove(string id)
        {
            //if (!_tasksDict.ContainsKey(id))
                return false;

            //_tasksDict.Remove(id);
            //return true;
        }

        public void Update(TaskItem item)
        {
            //if (!_tasksDict.ContainsKey(item.Id))
                return;

            //_tasksDict[item.Id] = item;
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
