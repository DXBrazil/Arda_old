using Arda.Permissions.Interfaces;
using Arda.Permissions.Models;
using System;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Arda.Permissions.ViewModels;
using Arda.Common.Utils;
using Arda.Common.Email;
using System.Collections.Generic;
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

        public void Seed()
        {
            //_context.Modules.Add(new Module() { ModuleName = "Infos", Endpoint = "http://localhost:2891/" });
            //_context.Modules.Add(new Module() { ModuleName = "Values", Endpoint = "http://localhost:2891/" });
            //_context.SaveChanges();
            //var mod1 = _context.Modules.First(m => m.ModuleName == "Infos");
            //var mod2 = _context.Modules.First(m => m.ModuleName == "Values");
            //_context.Resources.Add(new Resource() { ModuleID = mod1.ModuleID, ResourceName = "getinfo" });
            //_context.Resources.Add(new Resource() { ModuleID = mod2.ModuleID, ResourceName = "getvalues" });
            //_context.SaveChanges();
            var res =
                from r in _context.Resources
                where r.ResourceName == "index"
                select r;
            foreach (Resource r in res)
            {

            }
            var obj = _context.Resources.ToList();
        }


        public bool SetUserPermissionsAndCode(string uniqueName, string code)
        {
            try
            {
                var userPermissions = (from u in _context.Users
                                       join up in _context.UsersPermissions on u.UniqueName equals up.UniqueName
                                       join r in _context.Resources on up.ResourceID equals r.ResourceID
                                       join m in _context.Modules on r.ModuleID equals m.ModuleID
                                       where u.UniqueName == uniqueName
                                       select new PermissionsToBeCachedViewModel
                                       {
                                           Endpoint = m.Endpoint,
                                           Module = m.ModuleName,
                                           Resource = r.ResourceName,
                                           Category = r.Category,
                                           DisplayName = r.DisplayName
                                       }).ToList();

                var propertiesToCache = new CacheViewModel(code, userPermissions);
                _cache.Set(uniqueName, Util.GetBytes(propertiesToCache.ToString()));

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateUserPermissions(string uniqueName, ICollection<PermissionsToBeCachedViewModel> userPermission)
        {
            try
            {
                var propertiesSerializedCached = Util.GetString(_cache.Get(uniqueName));

                if (propertiesSerializedCached != null)
                {
                    var propertiesToCache = new CacheViewModel(propertiesSerializedCached);
                    propertiesToCache.Permissions = userPermission;

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
                var propertiesSerializedCached = Util.GetString(_cache.Get(uniqueName));
                if (propertiesSerializedCached != null)
                {
                    var permissions = new CacheViewModel(propertiesSerializedCached).Permissions;

                    var perm = from p in permissions
                               where p.Resource.Equals(resource) && p.Module.Equals(module)
                               select p;

                    if (perm != null)
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
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerifyIfUserIsInUserPermissionsDatabase(string uniqueName)
        {
            try
            {
                var response = _context.Users.SingleOrDefault(u => u.UniqueName == uniqueName);

                if (response == null)
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
        public User CreateNewUserAndSetInitialPermissions(string uniqueName)
        {
            try
            {
                var user = new User()
                {
                    UniqueName = uniqueName,
                    Status = PermissionStatus.Waiting_Review,
                    UserPermissions = new List<UsersPermission>()
                    {
                        new UsersPermission()
                        {
                            ResourceID=1
                        }
                    }
                };

                _context.Users.Add(user);
                var response = _context.SaveChanges();

                if (response >= 0)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetUserMenuSerialized(string uniqueName)
        {
            var menu = new List<Tuple<string, Tuple<string, string, string>>>();

            var propertiesSerializedCached = Util.GetString(_cache.Get(uniqueName));

            var permissions = new CacheViewModel(propertiesSerializedCached).Permissions;

            foreach (var p in permissions)
            {
                if (!p.Endpoint.Contains("/api"))
                {
                    string category = p.Category;
                    string display = p.DisplayName;
                    string controller = p.Module;
                    string action = p.Resource;
                    //string url = p.Endpoint + "/" + p.Module + "/" + p.Resource;

                    menu.Add(Tuple.Create(category, Tuple.Create(display, controller, action)));
                }
            }

            var menuGrouped = (from m in menu
                               group m.Item2 by m.Item1 into g
                               select new
                               {
                                   Category = g.Key,
                                   Items = g.ToList()
                               }).ToList();


            return JsonConvert.SerializeObject(menuGrouped);
        }

        public PermissionStatus GetUserStatus(string uniqueName)
        {
            try
            {
                return _context.Users.First(u => u.UniqueName == uniqueName).Status;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GetAdminUserStatus(string uniqueName)
        {
            try
            {
                var propertiesSerializedCached = Util.GetString(_cache.Get(uniqueName));
                var permissions = new CacheViewModel(propertiesSerializedCached).Permissions;

                var permToReview = permissions.First(p => p.Module == "Users" && p.Resource == "ReviewPermissions");
                if (permToReview != null)
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

        public int GetNumberOfUsersToApprove()
        {
            try
            {
                return _context.Users.Where(u => u.Status == PermissionStatus.Waiting_Review).Count();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}