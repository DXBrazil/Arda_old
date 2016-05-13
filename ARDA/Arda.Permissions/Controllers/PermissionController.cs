using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Permissions.Interfaces;
using Arda.Permissions.ViewModels;

namespace Arda.Permissions.Controllers
{
    [Route("api/permissions")]
    public class PermissionController : Controller
    {
        private IPermissionRepository _permission;

        public PermissionController(IPermissionRepository permission)
        {
            _permission = permission;
        }

        [HttpGet]
        [Route("getpermissionsetbyuseridandtoken")]
        public IActionResult GetPermissionSetByUserIDAndToken(string token)
        {
            try
            {
                var response = _permission.GetPermissionSetByUserIDAndToken(token);
                return Json(new { permissions = response.permissionsOfUser });
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("getuserpermissiontoresource")]
        public bool GetUserPermissionToResource(string token, string module, string resource)
        {
            try
            {
                var permissionResponse = _permission.VerifyUserAccessToResource(token, module, resource);

                if(permissionResponse)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
