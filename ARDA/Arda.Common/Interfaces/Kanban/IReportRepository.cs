using Arda.Common.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.Interfaces.Kanban
{
    public interface IReportRepository
    {
        IEnumerable<ActivityConsumingViewModel> GetActivityConsumingData(DateTime startDate, DateTime endDate, string user = "All");

        IEnumerable<CategoryConsumingViewModel> GetCategoryConsumingData(DateTime startDate, DateTime endDate, string user = "All");
        
    }
}
