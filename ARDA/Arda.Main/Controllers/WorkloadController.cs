using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Common.Utils;
using System.Net.Http;
using Arda.Common.ViewModels;
using Microsoft.AspNet.Authorization;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class WorkloadController : Controller
    {
        [HttpGet]
        public async Task<JsonResult> ListWorkloadsByUser()
        {
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            var workloads = new List<string>();

            try
            {
                var existentWorkloads = await Util.ConnectToRemoteService<List<WorkloadsByUserMainViewModel>>(HttpMethod.Get, Util.KanbanURL + "api/workload/listworkloadbyuser", uniqueName, "");

                var dados = existentWorkloads.Select(x => new { data = x._WorkloadID, value = x._WorkloadTitle + " (Started: " + x._WorkloadStartDate.ToString("dd/MM/yyyy") + " and Ending: " + x._WorkloadEndDate.ToString("dd/MM/yyyy") + ")" })
                                             .Distinct()
                                             .ToList();

                return Json(dados);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
