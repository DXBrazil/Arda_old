using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Net.Http;
using Arda.Common.Utils;
using Arda.Common.ViewModel;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class FiscalYearController : Controller
    {
        // List all fiscal years in database.
        public IActionResult List()
        {
            // Getting uniqueName
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            try
            {
                var response = Util.ConnectToRemoteService<List<FiscalYearViewModel>>("http://localhost:2768/api/fiscalyear/list", uniqueName, "", "get");
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }

            return View();
        }

        
    }
}
