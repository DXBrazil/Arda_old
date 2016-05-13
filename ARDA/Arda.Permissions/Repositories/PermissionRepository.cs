using Arda.Permissions.Interfaces;
using Arda.Permissions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arda.Common.JSON;
using Arda.Permissions.ViewModels;
using Microsoft.Extensions.Caching.Distributed;

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

        // Getting user permissions by UserID and Token.
        public TokenPermissionViewModel GetPermissionSetByUserIDAndToken(string token)
        {
            try
            {
                var permissions = _context.Permissions.FirstOrDefault(p => p.Token == token);

                if(permissions != null)
                {
                    return new TokenPermissionViewModel()
                    {
                        token = permissions.Token,
                        permissionsOfUser = permissions.PermissionsByUser
                    };
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        // Add permission for the first time.
        public bool AddFirstPermissionsSet(Guid userID, string token, string permissionsByUser)
        {
            return true;
        }

        // Update an existing permission to specific user.
        public bool UpdatePermssionsSetByUserIDAndToken(Guid userID, string token)
        {
            return true;
        }

        // Verifying if user (identified by user token) has authorization to specific resource.
        public bool VerifyUserAccessToResource(string token, string module, string resource)
        {
            // Logic here.
            return true;
        }

    }
}
