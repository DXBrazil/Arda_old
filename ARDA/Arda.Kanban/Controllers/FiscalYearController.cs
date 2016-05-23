using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;

namespace Arda.Kanban.Controllers
{
    [Route("api/[controller]")]
    public class FiscalYearController : Controller
    {
        [HttpPost]
        [Route("addnewfiscalyear")]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewFiscalYear()
        {
            // Logic here.
            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }
    }
}
