using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Common;
using Arda.Main.Repository.Emails;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Main.Controllers
{
    public class ClientAuthentication : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RequestNewAccount(string Name, string Email, string Phone, string Justification)
        {
            // Call the post from message.
            if(EmailRepository.SendEmailRequestNewAccount(Name, Email, Phone, Justification))
            {
                return Json(new { Status = "Ok" });
            }
            else
            {
                return Json(new { Status = "Fail" });
            }
        }
    }
}
