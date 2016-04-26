using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban
{
    class TaskRepository : ITaskRepository
    {
        TaskItem[] _tasks = new TaskItem[]
        {
            new TaskItem() { Id="t0", Name="Visual Studio Community" },
            new TaskItem() { Id="t1", Name="Visual Studio Enterprise Edition" },
            new TaskItem() { Id="t2", Name="VS Code" },
        };
        Dictionary<string, TaskItem> _tasksDict;

        public TaskRepository()
        {
            _tasksDict = new Dictionary<string, TaskItem>();
            foreach(var t in _tasks)
            {
                _tasksDict.Add(t.Id, t);
            }
            
        }

        public void Add(TaskItem item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _tasks;
        }

        public TaskItem GetById(string id)
        {
            if (!_tasksDict.ContainsKey(id))
                return null;

            return _tasksDict[id];
        }

        public bool Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(TaskItem item)
        {
            throw new NotImplementedException();
        }
    }
}
