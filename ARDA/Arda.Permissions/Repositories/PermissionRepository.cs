﻿using Arda.Common.Interfaces.Permissions;
using Arda.Common.Models.Permissions;
using System;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Arda.Common.ViewModels.Permissions;
using Arda.Common.Utils;
using Arda.Common.Email;
using System.Collections.Generic;
using Newtonsoft.Json;
using Arda.Common.ViewModels.Main;
using Arda.Kanban.Models;
using System.Net.Http;

namespace Arda.Permissions.Repositories
{
    //TODO: Splits in User and Permission Repository
    public class PermissionRepository : IPermissionRepository
    {
        private PermissionsContext _context;
        private IDistributedCache _cache;

        public PermissionRepository(PermissionsContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }


        public bool SetUserPermissionsAndCode(string uniqueName, string code)
        {
            try
            {
                var userPermissions = (from u in _context.Users
                                       join up in _context.UsersPermissions on u.UniqueName equals up.UniqueName
                                       join r in _context.Resources on up.ResourceID equals r.ResourceID
                                       join m in _context.Modules on r.ModuleID equals m.ModuleID
                                       where up.UniqueName == uniqueName && r.ResourceSequence > 0
                                       orderby r.CategorySequence, r.ResourceSequence
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

        //Updates permissiosn on database and cache
        public bool UpdateUserPermissions(string uniqueName, PermissionsViewModel newUserPermissions)
        {
            try
            {
                //Delete old permissions from the database:
                var oldPermissions = _context.UsersPermissions.Where(up => up.UniqueName == uniqueName);
                _context.UsersPermissions.RemoveRange(oldPermissions);
                //_context.SaveChanges();

                //Update the database
                //Permissions:
                foreach (var permissionToQuery in newUserPermissions.permissions)
                {
                    var resourceReturned = _context.Resources.First(r => r.Category == permissionToQuery.category && r.DisplayName == permissionToQuery.resource);

                    _context.UsersPermissions.Add(new UsersPermission()
                    {
                        UniqueName = uniqueName,
                        ResourceID = resourceReturned.ResourceID
                    });
                }
                //User:
                var user = _context.Users.First(u => u.UniqueName == uniqueName);
                if (newUserPermissions.permissions.Count > 0)
                {
                    user.Status = PermissionStatus.Permissions_Granted;
                }
                else
                {
                    user.Status = PermissionStatus.Permissions_Denied;
                }

                _context.SaveChanges();

                //Update the cache
                CacheViewModel propertiesToCache;
                try
                {
                    //User is on cache:
                    var propertiesSerializedCached = Util.GetString(_cache.Get(uniqueName));
                    propertiesToCache = new CacheViewModel(propertiesSerializedCached);
                }
                catch
                {
                    //User is not on cache:
                    propertiesToCache = new CacheViewModel();
                }

                var userPermissions = (from up in _context.UsersPermissions
                                       join r in _context.Resources on up.ResourceID equals r.ResourceID
                                       join m in _context.Modules on r.ModuleID equals m.ModuleID
                                       where up.UniqueName == uniqueName && r.ResourceSequence > 0
                                       orderby r.CategorySequence, r.ResourceSequence
                                       select new PermissionsToBeCachedViewModel
                                       {
                                           Endpoint = m.Endpoint,
                                           Module = m.ModuleName,
                                           Resource = r.ResourceName,
                                           Category = r.Category,
                                           DisplayName = r.DisplayName
                                       }).ToList();

                if (userPermissions != null)
                {
                    propertiesToCache.Permissions = userPermissions;
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

        public void DeleteUser(string uniqueName)
        {
            try
            {
                //From Cache:
                _cache.Remove(uniqueName);

                //From Context:
                var userPermissions = (from up in _context.UsersPermissions
                                       where up.UniqueName == uniqueName
                                       select up).ToList();

                var user = (from u in _context.Users
                            where u.UniqueName == uniqueName
                            select u).First();

                _context.UsersPermissions.RemoveRange(userPermissions);
                _context.Users.Remove(user);
                _context.SaveChanges();

                //From Kanban:
                var res = Util.ConnectToRemoteService(HttpMethod.Delete, Util.KanbanURL + "api/user/delete?userID=" + uniqueName, "kanban", "kanban").Result;

                if (!res.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
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

                    var perm = (from p in permissions
                                where p.Resource.ToLower().Equals(resource) && p.Module.ToLower().Equals(module)
                                select p).First();

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
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    return false;
                }
                else
                {
                    throw;
                }
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
        public User CreateNewUserAndSetInitialPermissions(string uniqueName, string name)
        {
            try
            {
                var user = new User()
                {
                    UniqueName = uniqueName,
                    Name = name,
                    Status = PermissionStatus.Waiting_Review,
                    UserPermissions = new List<UsersPermission>()
                };

                //Save on Permissions
                _context.Users.Add(user);
                _context.SaveChanges();

                //Save on Kanban
                var kanbanUser = new Common.ViewModels.Kanban.UserKanbanViewModel()
                {
                    UniqueName = user.UniqueName,
                    Name = user.Name
                };
                var res = Util.ConnectToRemoteService(HttpMethod.Post, Util.KanbanURL + "api/user/add", "kanban", "kanban",  kanbanUser).Result;

                if (res.IsSuccessStatusCode)
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

        public bool VerifyIfUserAdmin(string uniqueName)
        {
            try
            {
                var propertiesSerializedCached = Util.GetString(_cache.Get(uniqueName));
                var permissions = new CacheViewModel(propertiesSerializedCached).Permissions;

                var permToReview = permissions.First(p => p.Module == "Users" && p.Resource == "Review");
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

        public IEnumerable<UserMainViewModel> GetPendingUsers()
        {
            try
            {
                var data = from users in _context.Users
                           where users.Status == PermissionStatus.Waiting_Review
                           select new UserMainViewModel
                           {
                               Name = users.Name,
                               Email = users.UniqueName,
                               Status = (int)users.Status
                           };

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ResourcesViewModel> GetAllPermissions()
        {
            try
            {
                var data = (from r in _context.Resources
                            orderby r.CategorySequence, r.ResourceSequence
                            group r.DisplayName by r.Category into g
                            select new ResourcesViewModel
                            {
                                Category = g.Key,
                                Resources = g.ToList()
                            }).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PermissionsViewModel GetUserPermissions(string uniqueName)
        {
            try
            {
                var data = (from up in _context.UsersPermissions
                            join r in _context.Resources on up.ResourceID equals r.ResourceID
                            where up.UniqueName == uniqueName
                            orderby r.CategorySequence, r.ResourceSequence
                            select new Permission
                            {
                                category = r.Category,
                                resource = r.DisplayName
                            }).ToList();

                var permissions = new PermissionsViewModel()
                {
                    permissions = data
                };

                return permissions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<UserMainViewModel> GetUsers()
        {
            try
            {
                var data = from users in _context.Users
                           select new UserMainViewModel
                           {
                               Name = users.Name,
                               Email = users.UniqueName,
                               Status = (int)users.Status
                           };

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserMainViewModel GetUser(string uniqueName)
        {
            try
            {
                var data = (from user in _context.Users
                            where user.UniqueName == uniqueName
                            select new UserMainViewModel
                            {
                                Name = user.Name,
                                Email = user.UniqueName,
                                Status = (int)user.Status
                            }).First();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}