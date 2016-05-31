using System;
using Microsoft.AspNet.Mvc;
using Arda.Permissions.Interfaces;
using System.Net.Http;
using System.Net;

namespace Arda.Permissions.Controllers
{
    [Route("api/[controller]")]
    public class PermissionController : Controller
    {
        private IPermissionRepository _permission;

        public PermissionController(IPermissionRepository permission)
        {
            _permission = permission;
        }

        [HttpPost]
        [Route("setuserpermissionsandcode")]
        public IActionResult SetUserPermissionsAndCode()
        {
            var uniqueName = HttpContext.Request.Headers["unique_name"].ToString();
            var code = HttpContext.Request.Headers["code"].ToString();

            try
            {
                if (uniqueName != null && code != null)
                {
                    Models.User responseUser = null;
                    bool responseEmail = false;

                    bool UserExists = _permission.VerifyIfUserIsInUserPermissionsDatabase(uniqueName);
                    if (!UserExists)
                    {
                        responseUser = _permission.CreateNewUserAndSetInitialPermissions(uniqueName);
                        responseEmail = _permission.SendNotificationOfNewUserByEmail(uniqueName);
                        if (responseUser == null || responseEmail == false)
                        {
                            return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            bool response = _permission.SetUserPermissionsAndCode(uniqueName, code);
                            if (response)
                            {
                                return new HttpStatusCodeResult((int)HttpStatusCode.OK);
                            }
                            else
                            {
                                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                            }
                        }
                    }
                    else
                    {
                        bool response = _permission.SetUserPermissionsAndCode(uniqueName, code);
                        if (response)
                        {
                            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
                        }
                        else
                        {
                            return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                        }
                    }
                }
                else
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("updateuserpermissions")]
        public HttpResponseMessage UpdateUserPermissions(string uniqueName, string userPermissionsSerialized)
        {
            try
            {
                if (uniqueName != null && userPermissionsSerialized != null)
                {
                    if (_permission.SetUserPermissionsAndCode(uniqueName, userPermissionsSerialized))
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("deleteuserpermissions")]
        public HttpResponseMessage DeleteUserPermissions()
        {
            try
            {
                var uniqueName = HttpContext.Request.Headers["unique_name"].ToString();

                if (uniqueName != null)
                {
                    _permission.DeleteUserPermissions(uniqueName);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("verifyuseraccesstoresource")]
        public HttpResponseMessage VerifyUserAccessToResource(string uniqueName, string module, string resource)
        {
            try
            {
                if (uniqueName != null && module != null && resource != null)
                {
                    if (_permission.VerifyUserAccessToResource(uniqueName, module, resource))
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.Forbidden);
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        //[HttpGet]
        //[Route("seed")]
        //public void Seed()
        //{
        //    _permission.Seed();
        //}
    }
}