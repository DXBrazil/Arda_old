using Arda.Common.Interfaces.Kanban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arda.Common.ViewModels.Reports;
using Arda.Kanban.Models;

namespace Arda.Kanban.Repositories
{
    public class ReportRepository : IReportRepository
    {
        KanbanContext _context;

        public ReportRepository(KanbanContext context)
        {
            _context = context;
        }

        public IEnumerable<ActivityConsumingViewModel> GetActivityConsumingData(DateTime startDate, DateTime endDate, string user = "All")
        {
            try
            {
                List<ActivityConsumingViewModel> activities;
                if (user == "All")
                {
                    activities = (from a in _context.Activities
                                 join w in _context.WorkloadBacklogs on a.ActivityID equals w.WBActivity.ActivityID
                                 where w.WBStartDate >= startDate && w.WBEndDate <= endDate
                                 group 
                                 select new ActivityConsumingViewModel()
                                 {
                                     Activity = a.ActivityName
                                 }).ToList();

                }
                else
                {
                    activities = new List<ActivityConsumingViewModel>();
                }
                return activities;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
