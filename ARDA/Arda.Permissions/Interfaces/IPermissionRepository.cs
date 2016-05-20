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
        bool VerifyUserAccessToResource(string uniqueName, string module, string resource);

        // Verify if user exists in UserPermissions table.
        bool VerifyIfUserIsInUserPermissionsDatabase(string uniqueName);

        // Send a notification about new user to administrator.
        bool SendNotificationOfNewUserByEmail(string uniqueName);

        // Set basic permissions to new users.
        bool SetPermissionsToNewUsers(string uniqueName);

        //void SetAllan();
    }
}