using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public IActionResult ReviewPermissions()
        {
            return View();
        }

        public IActionResult ListBannedUsers()
        {
            return View();
        }


    }
}
