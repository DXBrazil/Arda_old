using Arda.Permissions.Interfaces;
using Arda.Permissions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arda.Common.JSON;
using Arda.Permissions.ViewModels;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;

namespace Arda.Permissions.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private PermissionsContext _context;
        private IDistributedCache _cache;

        public PermissionRepository(PermissionsContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public bool SetUserProperties(string authCode, string uniqueName)
        {
            try
            {
                var userProperties = _context.UserProperties.SingleOrDefault(u => u.AuthCode == authCode && u.UniqueName == uniqueName);
                if (userProperties != null)
                {
                    userProperties.AuthCode = authCode;
                    _cache.Set(uniqueName, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userProperties)));
                }

                var response = JsonConvert.DeserializeObject<UserProperties>(Encoding.UTF8.GetString(_cache.Get(uniqueName)));
                if (response == userProperties)
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

        public bool UpdateUserProperties(string uniqueName, UserProperties properties)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserProperties(string uniqueName)
        {
            throw new NotImplementedException();
        }

        public bool VerifyUserAccessToResource(string uniqueName, UserProperties resource)
        {
            throw new NotImplementedException();
        }
    }
}
