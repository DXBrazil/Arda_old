using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Common.ViewModels.Reports;
using Arda.Common.Interfaces.Kanban;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Kanban.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        IReportRepository _repository;

        public ReportController(IReportRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getactivityconsumingdata")]
        public IEnumerable<ActivityConsumingViewModel> GetActivityConsumingData(DateTime startDate, DateTime endDate, string user = "All")
        {
            try
            {
                var activities = _repository.GetActivityConsumingData(startDate, endDate, user);

                if (activities != null)
                {
                    return activities;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("getexpertiseconsumingdata")]
        public IEnumerable<ExpertiseConsumingViewModel> GetExpertiseConsumingData(DateTime startDate, DateTime endDate, string user = "All")
        {
            try
            {
                var categories = _repository.GetExpertiseConsumingData(startDate, endDate, user);

                if (categories != null)
                {
                    return categories;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
