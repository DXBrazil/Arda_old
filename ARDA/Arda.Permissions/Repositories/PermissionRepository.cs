using Arda.Permissions.Interfaces;
using Arda.Permissions.Models;
using System;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Arda.Permissions.ViewModels;
using Arda.Common.Utils;
using Arda.Common.Email;

namespace Arda.Permissions.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private UserPermissionsContext _context;
        private IDistributedCache _cache;

        public PermissionRepository(UserPermissionsContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public bool SetUserPermissionsAndCode(string uniqueName, string code)
        {
            try
            {
                var userProperties = _context.UsersPermissions.SingleOrDefault(user => user.UniqueName == uniqueName);
                if (userProperties != null)
                {
                    var permissions = userProperties.ToPermission();
                    var propertiesToCache = new UserPropertiesCachedViewModel(code, permissions);

                    _cache.Set(uniqueName, Util.GetBytes(propertiesToCache.ToString()));
                    return true;
                }
                else
                {
                    bool setPermissionsResponse = SetPermissionsToNewUsers(uniqueName);

                    if (setPermissionsResponse)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateUserPermissions(string uniqueName, string userPermissionsSerialized)
        {
            try
            {
                var propertiesSerializedCached = Util.GetString(_cache.Get(uniqueName));

                if (propertiesSerializedCached != null)
                {
                    var propertiesToCache = new UserPropertiesCachedViewModel(propertiesSerializedCached);
                    propertiesToCache.Permissions = new PermissionsScope(userPermissionsSerialized);

                    _cache.Set(uniqueName, Util.GetBytes(propertiesToCache.ToString()));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUserPermissions(string uniqueName)
        {
            try
            {
                _cache.Remove(uniqueName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerifyUserAccessToResource(string uniqueName, string module, string resource)
        {
            try
            {
                resource = resource.ToLower();
                module = module.ToLower();

                var propertiesSerializedCached = Util.GetString(_cache.Get(uniqueName));
                if (propertiesSerializedCached != null)
                {
                    var permissions = new UserPropertiesCachedViewModel(propertiesSerializedCached).Permissions.ToString().Trim().ToLower();
                    if (permissions.Contains(module) && permissions.Contains(resource))
                    {
                        var search = "module\":\"" + module + "\",\"resource\":\"" + resource + "\",\"enabled\":";
                        var permExtracted = permissions.Substring(permissions.IndexOf(search) + search.Length);
                        var value = permExtracted.Substring(0, permExtracted.IndexOf("}"));
                        if (bool.Parse(value))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerifyIfUserIsInUserPermissionsDatabase(string uniqueName)
        {
            try
            {
                var response = _context.UsersPermissions.SingleOrDefault(u => u.UniqueName == uniqueName);

                if(response == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool SendNotificationOfNewUserByEmail(string uniqueName)
        {
            EmailLogic clientEmail = new EmailLogic();

            // Mounting parameters and message.
            string FromName = "Arda Team";
            string FromEmail = "arda@microsoft.com";
            string ToName = "Arda Administrator";
            string ToEmail = "fabsanc@microsoft.com";
            string Subject = "[ARDA] A new user has been logged @ Arda";

            StringBuilder StructureModified = new StringBuilder();
            StructureModified = EmailMessages.GetEmailMessageStructure();

            // Replacing the generic title by the customized.
            StructureModified.Replace("[MessageTitle]", "Hi " + ToName + ", someone requested access to the system");

            // Replacing the generic subtitle by the customized.
            StructureModified.Replace("[MessageSubtitle]", "Who did the request was <strong>" + uniqueName + "</strong>. If you deserve, can access 'Users' area and distribute the appropriated permissions.");

            // Replacing the generic message body by the customized.
            StructureModified.Replace("[MessageBody]", "Details about the request: </br></br><ul><li>Email: " + uniqueName + "</li><li>Date/time access: " + DateTime.Now + "</li></ul>");

            // Replacing the generic callout box.
            StructureModified.Replace("[MessageCallout]", "For more details about the request, send a message to <strong>arda@microsoft.com</strong>.");

            // Creating a object that will send the message.
            EmailLogic EmailObject = new EmailLogic();

            try
            {
                var EmailTask = EmailObject.SendEmailAsync(FromName, FromEmail, ToName, ToEmail, Subject, StructureModified.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Generate initial and basic permissions set to new users.
        public bool SetPermissionsToNewUsers(string _uniqueName)
        {
            PermissionsScope perm = new PermissionsScope();
            perm.Permissions.Add(new Permission() { Module = "Dashboard", Resource = "Details", Enabled = true });

            _context.UsersPermissions.Add(new UsersPermissions()
            {
                UniqueName = _uniqueName,
                PermissionsSerialized = perm.ToString()
            });

            var response = _context.SaveChanges();
            
            if(response >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}