using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Permissions.Interfaces;

namespace Arda.Permissions.Controllers
{
    [Route("api/[controller]")]
    public class UserOperationsController : Controller
    {

        private IPermissionRepository _permission;

        public UserOperationsController(IPermissionRepository permission)
        {
            _permission = permission;

        }


        [HttpGet]
        [Route("getusermenu")]
        public string GetUserMenu()
        {
            var uniqueName = HttpContext.Request.Headers["unique_name"].ToString();

            try
            {
                if (uniqueName != null)
                {
                    var menu = _permission.GetUserMenuSerialized(uniqueName);
                    return menu;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getuserstatus")]
        public int GetUserStatus()
        {
            var uniqueName = HttpContext.Request.Headers["unique_name"].ToString();

            try
            {
                if (uniqueName != null)
                {
                    return (int)_permission.GetUserStatus(uniqueName);
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getadminuserstatus")]
        public bool GetAdminUserStatus()
        {
            var uniqueName = HttpContext.Request.Headers["unique_name"].ToString();

            try
            {
                if (uniqueName != null)
                {
                    return _permission.GetAdminUserStatus(uniqueName);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getnumberofuserstoapprove")]
        public int GetNumberOfUsersToApprove()
        {
            try
            {
                return _permission.GetNumberOfUsersToApprove();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
