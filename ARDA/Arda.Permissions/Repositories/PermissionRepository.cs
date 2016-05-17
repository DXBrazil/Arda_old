using Arda.Permissions.Interfaces;
using Arda.Permissions.Models;
using System;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Arda.Permissions.ViewModels;
using Arda.Common.Utils;

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
                    return false;
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
                    propertiesToCache.Permissions = new Permission(userPermissionsSerialized);

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

        public bool VerifyUserAccessToResource(string uniqueName, string resource)
        {
            try
            {
                resource = resource.ToLower();
                var propertiesSerializedCached = Util.GetString(_cache.Get(uniqueName));

                if (propertiesSerializedCached != null)
                {
                    var permissions = new UserPropertiesCachedViewModel(propertiesSerializedCached).Permissions.ToString().Trim().ToLower();
                    if (permissions.Contains(resource))
                    {
                        var search = "\"module\":\"" + resource + "\",\"enabled\":";
                        var permExtracted = permissions.Substring(permissions.IndexOf(search) + search.Length);
                        var value = permExtracted.Substring(0, permExtracted.IndexOf(","));
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
   

        //private byte[] GetBytes(string obj)
        //{
        //    return Encoding.UTF8.GetBytes(obj);
        //}

        //private string GetString(byte[] obj)
        //{
        //    return Encoding.UTF8.GetString(obj);
        //}

    }
}

//public void SetAllan()
//{
//    Permission perm = new Permission()
//    {
//        Module = "Dashboard",
//        Enabled = true,
//        NestedPermissions = new List<Permission>()
//                {
//                    new Permission("page1", false),
//                    new Permission("page2", false),
//                    new Permission("page3", true)
//                }
//    };

//    _context.UsersPermissions.Add(new UsersPermissions()
//    {
//        UniqueName = "fabsanc@microsoft.com",
//        PermissionsSerialized = perm.ToString()
//    });
//    _context.SaveChanges();
//}