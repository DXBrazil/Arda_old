using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Common.ViewModels.Main;
using Arda.Common.Interfaces.Kanban;


namespace Arda.Kanban.Controllers
{
    [Route("api/[controller]")]
    public class WorkloadController : Controller
    {
        IWorkloadRepository _repository;

        public WorkloadController(IWorkloadRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("listworkloadbyuser")]
        public IEnumerable<WorkloadsByUserViewModel> ListWorkloadByUser()
        {
            try
            {
                var uniqueName = HttpContext.Request.Headers["unique_name"].ToString();
                var workloads = _repository.GetWorkloadsByUser(uniqueName);

                if (workloads != null)
                {
                    return workloads;
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
