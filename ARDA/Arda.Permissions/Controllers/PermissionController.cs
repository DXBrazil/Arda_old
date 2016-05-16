using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Permissions.Interfaces;
using Arda.Permissions.ViewModels;

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

        [HttpGet]
        [Route("setuserproperties")]
        public bool SetUserProperties(string authCode, string uniqueName)
        {
            try
            {
                var response = _permission.SetUserProperties(authCode, uniqueName);

                if (response)
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
