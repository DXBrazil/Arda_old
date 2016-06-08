using Arda.Common.ViewModels.Main;
using Arda.Common.Models.Permissions;
using System.Collections.Generic;

namespace Arda.Common.Interfaces.Permissions
{
    public interface IPermissionRepository
    {
        // Save the permissions and code at the cache.
        bool SetUserPermissionsAndCode(string uniqueName, string code);

        // Update an existing user permissions.
        bool UpdateUserPermissions(string uniqueName, PermissionsViewModel userPermission);

        // Delete an existing user permissions from the cache.
        void DeleteUserPermissions(string uniqueName);

        void DeleteUser(string uniqueName);

        // Verify if user has authorization to specific resource.
        bool VerifyUserAccessToResource(string uniqueName, string module, string resource);

        // Verify if user exists in UserPermissions table.
        bool VerifyIfUserIsInUserPermissionsDatabase(string uniqueName);

        // Send a notification about new user to administrator.
        bool SendNotificationOfNewUserByEmail(string uniqueName);

        // Set basic permissions to new users.
        User CreateNewUserAndSetInitialPermissions(string uniqueName, string name);

        // Create the user menu based on his/her permissions.
        string GetUserMenuSerialized(string uniqueName);

        PermissionStatus GetUserStatus(string uniqueName);

        bool VerifyIfUserAdmin(string uniqueName);

        int GetNumberOfUsersToApprove();

        IEnumerable<UserViewModel> GetPendingUsers();

        IEnumerable<ResourcesViewModel> GetAllPermissions();

        PermissionsViewModel GetUserPermissions(string uniqueName);

        IEnumerable<UserViewModel> GetUsers();

        UserViewModel GetUser(string uniqueName);
    }
}