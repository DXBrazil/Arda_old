using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Common.Utils;

namespace Arda.Main.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Add()
        {
            ViewBag.Guid = Util.GenerateNewGuid();
            return View();
        }
    }
}
