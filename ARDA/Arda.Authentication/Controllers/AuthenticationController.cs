using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Authentication.ViewModels.Authentication;
using Arda.Authentication.Interfaces;
using Arda.Authentication.Models;
using System.Net.Http;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Authentication.Controllers
{
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private IAuthentication _authentication;
        HttpClient client;

        public AuthenticationController(IAuthentication authentication)
        {
            _authentication = authentication;
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2884/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
                    // Get user permissions based on his token.
                    string url = client.BaseAddress + "permissions/getpermissionsetbyuseridandtoken?token=" + user.Token;
                    var response = client.GetAsync(url).Result;

                    if (user.Status == 1 && response.IsSuccessStatusCode)
                    {
                        // Register token and user permitions in Azure Redis Cache.
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