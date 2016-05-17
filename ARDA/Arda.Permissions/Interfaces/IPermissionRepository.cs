using Arda.Permissions.Models;

namespace Arda.Permissions.Interfaces
{
    public interface IPermissionRepository
    {
        // Save the permissions and code at the cache.
        bool SetUserPermissionsAndCode(string uniqueName, string code);

        // Update an existing user permissions.
        bool UpdateUserPermissions(string uniqueName, string userPermissionsSerialized);

        // Delete an existing user permissions from the cache.
        void DeleteUserPermissions(string uniqueName);

        // Verify if user has authorization to specific resource.
        bool VerifyUserAccessToResource(string uniqueName, string resource);
    }
}