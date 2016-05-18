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
        public HttpResponseMessage SetUserPermissionsAndCode()
        {
            var uniqueName = HttpContext.Request.Headers["unique_name"].ToString();
            var code = HttpContext.Request.Headers["code"].ToString();

            try
            {
                if (uniqueName != null && code != null)
                {
                    if (_permission.SetUserPermissionsAndCode(uniqueName, code))
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
                if (uniqueName != null && module!=null && resource != null)
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
        //[Route("setallan")]
        //public void SetAllan()
        //{
        //    _permission.SetAllan();
        //}
    }
}