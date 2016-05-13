using Arda.Permissions.Models;
using Arda.Permissions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.Interfaces
{
    public interface IPermissionRepository
    {
        // Require user permissions by userID and token.
        TokenPermissionViewModel GetPermissionSetByUserIDAndToken(string token);

        // Save the first permission at database.
        bool AddFirstPermissionsSet(Guid userID, string token, string permissionsByUser);

        // Update an existing user permission.
        bool UpdatePermssionsSetByUserIDAndToken(Guid userID, string token);

        // Verify if user (identified by user token) has authorization to specific resource.
        bool VerifyUserAccessToResource(string token, string module, string resource);
    }
}