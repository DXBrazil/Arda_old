using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Net.Http;
using Arda.Common.Utils;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class FiscalYearController : Controller
    {
        // List all fiscal years in database.
        public IActionResult List()
        {
            // Getting unique_name and code
            string uniqueName = User.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
            string code = User.Claims.First(claim => claim.Type == "code").Value;

            var response = Util.ConnectToRemoteService("http://localhost:2768/fiscalyear/list?numberOfOccurecies=10", uniqueName, code, "get");

            return View();
        }

        
    }
}
