using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult UserAdd()
        {
            return View();
        }

        public IActionResult UserRemove()
        {
            return View();
        }
    }
}
