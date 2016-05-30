using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Arda.Main.Utils;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNet.Authentication.OpenIdConnect;
using System.IO;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public async Task<IActionResult> ReviewPermissions()
        {

            AuthenticationResult result = null;

            try
            {
                string userObjectID = HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
                AuthenticationContext authContext = new AuthenticationContext(Startup.Authority, new SessionCache(userObjectID, HttpContext));
                ClientCredential credential = new ClientCredential(Startup.ClientId, Startup.ClientSecret);
                result = await authContext.AcquireTokenSilentAsync(Startup.GraphResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));

                ViewBag.Token = result.AccessToken;
                return View();
                //HttpClient client = new HttpClient();
                //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/users/maluz@microsoft.com/photo/$value");
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                //HttpResponseMessage response = await client.SendAsync(request);

                //if (response.IsSuccessStatusCode)
                //{
                //    var source = await response.Content.ReadAsByteArrayAsync();
                //    return View();
                //}
                //else
                //{
                //    //
                //    // If the call failed with access denied, then drop the current access token from the cache, 
                //    //     and show the user an error indicating they might need to sign-in again.
                //    //
                //    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                //    {

                //        //var todoTokens = authContext.TokenCache.ReadItems().Where(a => a.Resource == Startup.TodoListResourceId);
                //        //foreach (TokenCacheItem tci in todoTokens)
                //        //    authContext.TokenCache.DeleteItem(tci);

                //        //ViewBag.ErrorMessage = "UnexpectedError";
                //    }
                //}
            }
            catch (Exception)
            {
                //If get silent token fails:
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
            }
        }

        public IActionResult ListBannedUsers()
        {
            return View();
        }

        private async Task<IActionResult> callMicrosoftGraph()
        {
            AuthenticationResult result = null;

            try
            {
                string userObjectID = HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
                AuthenticationContext authContext = new AuthenticationContext(Startup.Authority, new SessionCache(userObjectID, HttpContext));
                ClientCredential credential = new ClientCredential(Startup.ClientId, Startup.ClientSecret);
                result = await authContext.AcquireTokenSilentAsync(Startup.GraphResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));

                ViewBag.Token = result.AccessToken;
                return View();
                //HttpClient client = new HttpClient();
                //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/users/maluz@microsoft.com/photo/$value");
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                //HttpResponseMessage response = await client.SendAsync(request);

                //if (response.IsSuccessStatusCode)
                //{
                //    var source = await response.Content.ReadAsByteArrayAsync();
                //    return View();
                //}
                //else
                //{
                //    //
                //    // If the call failed with access denied, then drop the current access token from the cache, 
                //    //     and show the user an error indicating they might need to sign-in again.
                //    //
                //    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                //    {

                //        //var todoTokens = authContext.TokenCache.ReadItems().Where(a => a.Resource == Startup.TodoListResourceId);
                //        //foreach (TokenCacheItem tci in todoTokens)
                //        //    authContext.TokenCache.DeleteItem(tci);

                //        //ViewBag.ErrorMessage = "UnexpectedError";
                //    }
                //}
            }
            catch (Exception)
            {
                //If get silent token fails:
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
            }


            return View("Error");
        }
    }
}
