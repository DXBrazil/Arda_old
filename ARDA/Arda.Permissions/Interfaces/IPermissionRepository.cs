using Arda.Permissions.Models;

namespace Arda.Permissions.Interfaces
{
    public interface IPermissionRepository
    {
        // Save the first permission at database.
        bool SetUserProperties(string authCode, string uniqueName);

        // Update an existing user permission.
        bool UpdateUserProperties(string uniqueName, UserProperties properties);

        // Delete an existing user permission.
        void DeleteUserProperties(string uniqueName);

        // Verify if user (identified by user token) has authorization to specific resource.
        bool VerifyUserAccessToResource(string uniqueName, UserProperties resource);
    }
}