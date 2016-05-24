using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class FiscalYearController : Controller
    {
        // List all fiscal years in database.
        public IActionResult List()
        {
            return View();
        }
    }
}
