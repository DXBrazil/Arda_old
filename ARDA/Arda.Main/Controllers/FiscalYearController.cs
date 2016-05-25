using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Net.Http;
using Arda.Common.Utils;
using Arda.Common.ViewModel;
using Arda.Main.ViewModel;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class FiscalYearController : Controller
    {
        // List all fiscal years in database.
        public IActionResult Index()
        {
            // Getting uniqueName
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            List<FiscalYearMainViewModel> responseTreated;

            try
            {
                var response = Util.ConnectToRemoteService<List<FiscalYearViewModel>>("http://localhost:2768/api/fiscalyear/list", uniqueName, "", "get");
                responseTreated = (from u in response
                                   select new FiscalYearMainViewModel
                                   {
                                       FiscalYearID = u.FiscalYearID,
                                       FullNumericFiscalYearMain = u.FullNumericFiscalYear,
                                       TextualFiscalYearMain = u.TextualFiscalYear
                                  }).ToList();
            }
            catch (Exception e)
            {
                throw;
            }

            return View(responseTreated);
        }
    }
}
