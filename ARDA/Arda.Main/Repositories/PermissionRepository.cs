using Arda.Main.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arda.Main.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private IDistributedCache _cache;

        public PermissionRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public bool WriteTokenAndUserPermissions(string uniqueName, string authorizationCode)
        {
            try
            {
                // Setting data into redis cache.
                _cache.Set(uniqueName, Encoding.UTF8.GetBytes(authorizationCode));

                // Retrieving cache data to test de operation.
                var response = _cache.Get(uniqueName);

                if (authorizationCode == Encoding.UTF8.GetString(response))
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
