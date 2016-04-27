using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban
{
    public interface ITaskRepository
    {
        TaskItem Add(TaskItem item);
        IEnumerable<TaskItem> GetAll();
        TaskItem GetById(string id);
        void Update(TaskItem item);
        bool Remove(string id);
    }
}
