using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban
{
    class TaskRepository : ITaskRepository
    {
        Dictionary<string, TaskItem> _tasksDict;

        public TaskRepository()
        {
            // Fake data
            _tasksDict = new Dictionary<string, TaskItem>();
            Add(new TaskItem() { Name = "Visual Studio Community" });
            Add(new TaskItem() { Name = "Visual Studio Enterprise Edition" });
            Add(new TaskItem() { Name = "VS Code" });
        }

        private int _newId = 0;

        string NewId()
        {
            return "t" + (_newId++);
        }

        public void Add(TaskItem item)
        {
            string id = item.Id = NewId();

            _tasksDict.Add(id, item);
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _tasksDict.Values;
        }

        public TaskItem GetById(string id)
        {
            if (!_tasksDict.ContainsKey(id))
                return null;

            return _tasksDict[id];
        }

        public bool Remove(string id)
        {
            if (!_tasksDict.ContainsKey(id))
                return false;

            _tasksDict.Remove(id);
            return true;
        }

        public void Update(TaskItem item)
        {
            if (!_tasksDict.ContainsKey(item.Id))
                return;

            _tasksDict[item.Id] = item;
        }
    }
}
