using Arda.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Interfaces
{
    public interface IWorkloadRepository
    {
        List<WorkloadsByUserMainViewModel> GetWorkloadsByUser(string uniqueName);
    }
}
