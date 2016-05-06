using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Authentication.ViewModels.Authentication;
using Arda.Authentication.Interfaces;
using Arda.Authentication.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Authentication.Controllers
{
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private IAuthentication _authentication;

        public AuthenticationController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost]
        [Route("userauthentication")]
        public IActionResult UserAuthentication(AuthenticationViewModel viewModel)
        {
            User user = _authentication.GetUserByEmailAndPassword(viewModel.email, viewModel.password);

            try
            {
                if(user == null)
                {
                    return Json(new { Status = "Fail" });
                }
                else
                {
                    if(user.Status == 1)
                    {
                        // Requires access token to verify permissions.
                        // Call here.

                        return Json(new { Status = "Ok" });
                    }
                    else
                    {
                        return Json(new { Status = "Inactive" });
                    }
                }
            }
            catch (Exception)
            {
                return Json(new { Status = "Fail" });
            }
        }
    }
}