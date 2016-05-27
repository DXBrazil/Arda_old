using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Main.Infra;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNet.Authorization;
using Newtonsoft.Json;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
