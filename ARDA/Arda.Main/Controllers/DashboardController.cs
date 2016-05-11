using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Main.Infra;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Main.Controllers
{
    public class DashboardController : Controller
    {
        [LayoutInjecter("_LayoutInterno")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
