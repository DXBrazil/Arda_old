using Arda.Common.ViewModels.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.Interfaces.Kanban
{
    public interface IWorkloadRepository
    {
        List<WorkloadsByUserViewModel> GetWorkloadsByUser(string uniqueName);
    }
}
