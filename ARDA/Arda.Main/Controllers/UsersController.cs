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
using Arda.Common.ViewModels;
using Arda.Common.Utils;
using Newtonsoft.Json;
using System.Net;

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



        public async Task<List<PendingUsersViewModel>> PendingUsers()
        {
            var uniqueName = User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            var users = await Util.ConnectToRemoteService< List<PendingUsersViewModel>>(HttpMethod.Get, Util.PermissionsURL + "api/useroperations/getpendingusers", uniqueName, "");
            return users;
        }

        public async Task<List<ResourcesViewModel>> ResourceItems()
        {
            var uniqueName = User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            var items = await Util.ConnectToRemoteService<List<ResourcesViewModel>>(HttpMethod.Get, Util.PermissionsURL + "api/permission/getallpermissions", uniqueName, "");
            return items;
        }

        public async Task<HttpResponseMessage> UpdatePermissions(string user)
        {

            var reader = new StreamReader(HttpContext.Request.Body,System.Text.Encoding.UTF8);
            var requestFromAJAX = reader.ReadToEnd();
            //Check object integrity:
            var obj = JsonConvert.DeserializeObject<PermissionsViewModel>(requestFromAJAX);

            if (user != null && obj!=null)
            {
                var uniqueName = User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                var response = await Util.ConnectToRemoteService<PermissionsViewModel>(HttpMethod.Put, Util.PermissionsURL + "api/permission/updateuserpermissions?uniqueName=" + user, uniqueName, "", obj);

                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        //public async Task<HttpResponseMessage> GetUserPermissions(string user)
        //{
        //    if (user != null)
        //    {
        //        var uniqueName = User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
        //        var response = await Util.ConnectToRemoteService<PermissionsViewModel>(HttpMethod.Get, Util.PermissionsURL + "/api/UserOperations/GetUserPermissions?uniqueName=" + user, uniqueName, "");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return new HttpResponseMessage(HttpStatusCode.OK);
        //        }
        //        else
        //        {
        //            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //        }
        //    }
        //    return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //}



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


            //return View("Error");
        }
    }
}
