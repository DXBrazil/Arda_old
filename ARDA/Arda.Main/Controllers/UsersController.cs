using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Arda.Main.Utils;
using System.Net.Http;
using Microsoft.AspNet.Authentication.OpenIdConnect;
using System.IO;
using Arda.Common.ViewModels;
using Arda.Common.Utils;
using Newtonsoft.Json;
using System.Net;
using Arda.Common.JSON;

//TODO: Refactor name Users to User
namespace Arda.Main.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        #region Views

        //All Users
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await getAccessToken();
                ViewBag.Token = result.AccessToken;

                return View();
            }
            catch (Exception)
            {
                //If get silent token fails:
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
            }
        }

        //Review Permissions:
        public async Task<IActionResult> Review()
        {
            try
            {
                var result = await getAccessToken();
                ViewBag.Token = result.AccessToken;

                return View();
            }
            catch (Exception)
            {
                //If get silent token fails:
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
            }
        }

        //User Details:
        public async Task<IActionResult> Details(string userID)
        {
            try
            {
                var result = await getAccessToken();
                ViewBag.Token = result.AccessToken;

                var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                var user = await Util.ConnectToRemoteService<UserMainViewModel>(HttpMethod.Get, Util.PermissionsURL + "api/useroperations/getuser?uniqueName=" + userID, uniqueName, "");
                return View(user);
            }
            catch (Exception)
            {
                //If get silent token fails:
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
            }
        }

        //Edit User:
        public async Task<IActionResult> Edit(string userID)
        {
            try
            {
                var result = await getAccessToken();
                ViewBag.Token = result.AccessToken;

                var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                var user = await Util.ConnectToRemoteService<UserMainViewModel>(HttpMethod.Get, Util.PermissionsURL + "api/useroperations/getuser?uniqueName=" + userID, uniqueName, "");
                return View(user);
            }
            catch (Exception)
            {
                //If get silent token fails:
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
            }
        }

        #endregion

        #region Actions

        public async Task<JsonResult> ListAllUsers()
        {
            // Getting uniqueName
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            // Creating the final expected object to datatable
            SourceDataTablesFormat dataTablesSource = new SourceDataTablesFormat();

            try
            {
                // Getting the response of remote service
                var existentUsers = await Util.ConnectToRemoteService<List<UserMainViewModel>>(HttpMethod.Get, Util.PermissionsURL + "api/useroperations/getusers", uniqueName, "");

                // Mouting rows data
                foreach (UserMainViewModel user in existentUsers)
                {
                    IList<string> dataRow = new List<string>();
                    dataRow.Add(user.Name.ToString());
                    dataRow.Add(user.Email.ToString());
                    dataRow.Add(getUserSituation(user.Status));
                    dataRow.Add($"<div class='data-sorting-buttons'><a href='/users/details?userID={user.Email}' class='ds-button-detail'><i class='fa fa-align-justify' aria-hidden='true'></i>Details</div></a><div class='data-sorting-buttons'><a href='/users/edit?userID={user.Email}' class='ds-button-edit'><i class='fa fa-pencil-square-o' aria-hidden='true'></i>Edit</a></div><div class='data-sorting-buttons'><a data-toggle='modal' data-target='#DeleteUserModal' onclick=\"ModalDeleteUser('{user.Email}','{user.Name}');\" class='ds-button-delete'><i class='fa fa-trash' aria-hidden='true'></i>Delete</a></div>");
                    dataTablesSource.aaData.Add(dataRow);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Json(dataTablesSource);
        }

        public async Task<JsonResult> ListPendingUsers()
        {
            var uniqueName = User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            SourceDataTablesFormat dataTablesSource = new SourceDataTablesFormat();

            try
            {
                var users = await Util.ConnectToRemoteService<List<UserMainViewModel>>(HttpMethod.Get, Util.PermissionsURL + "api/useroperations/getpendingusers", uniqueName, "");

                // Mouting rows data
                foreach (UserMainViewModel user in users)
                {
                    IList<string> dataRow = new List<string>();
                    dataRow.Add(user.Name.ToString());
                    dataRow.Add(user.Email.ToString());
                    dataRow.Add($"<a onclick=\"ModalSelectUser('{user.Email}','{user.Name}');\" class='btn btn-info'><i class='fa fa-toggle-right' aria-hidden='true'></i></a>");
                    dataTablesSource.aaData.Add(dataRow);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Json(dataTablesSource);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(string user)
        {
            if (user != null)
            {
                var uniqueName = User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                var response = await Util.ConnectToRemoteService(HttpMethod.Delete, Util.PermissionsURL + "api/permission/deleteuser?uniqueName=" + user, uniqueName, "");

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

        [HttpPut]
        public async Task<HttpResponseMessage> Update(string user)
        {

            var reader = new StreamReader(HttpContext.Request.Body, System.Text.Encoding.UTF8);
            var requestFromAJAX = reader.ReadToEnd();
            //Check object integrity:
            var obj = JsonConvert.DeserializeObject<PermissionsViewModel>(requestFromAJAX);

            if (user != null && obj != null)
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

        public async Task<JsonResult> GetAllResources()
        {
            try
            {
                var uniqueName = User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                var items = await Util.ConnectToRemoteService<List<ResourcesViewModel>>(HttpMethod.Get, Util.PermissionsURL + "api/permission/getallpermissions", uniqueName, "");
                return Json(items);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<JsonResult> GetUserPermissions(string user)
        {
            if (user != null)
            {
                try
                {
                    var uniqueName = User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                    var permissions = await Util.ConnectToRemoteService<PermissionsViewModel>(HttpMethod.Get, Util.PermissionsURL + "/api/UserOperations/GetUserPermissions?uniqueName=" + user, uniqueName, "");
                    return Json(permissions);
                }
                catch (Exception)
                {
                    throw;
                }
            }else
            {
                return null;
            }
        }

        #endregion

        #region Utils

        private async Task<AuthenticationResult> getAccessToken()
        {
            string userObjectID = HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            AuthenticationContext authContext = new AuthenticationContext(Startup.Authority, new SessionCache(userObjectID, HttpContext));
            ClientCredential credential = new ClientCredential(Startup.ClientId, Startup.ClientSecret);
            return await authContext.AcquireTokenSilentAsync(Startup.GraphResourceId, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));
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


            //return View("Error");
        }

        private string getUserSituation(int situation)
        {
            switch (situation)
            {
                case 0:
                    return "Waiting Review";
                case 1:
                    return "Permissions Denied";
                case 2:
                    return "Permissions Granted";
                default:
                    return "";
            }
        }

        #endregion

    }
}
