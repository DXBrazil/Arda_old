using Arda.Permissions.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.ViewModels
{
    public class UserPropertiesCachedViewModel
    {
        public string Code { get; set; }

        public PermissionsScope Permissions { get; set; }

        public UserPropertiesCachedViewModel() { }

        public UserPropertiesCachedViewModel(string propertiesCachedSerialized)
        {
            var prop = JsonConvert.DeserializeObject<UserPropertiesCachedViewModel>(propertiesCachedSerialized);
            Code = prop.Code;
            Permissions = prop.Permissions;
        }

        public UserPropertiesCachedViewModel(PermissionsScope permissions)
        {
            Code = "";
            Permissions = permissions;
        }

        public UserPropertiesCachedViewModel(string code, PermissionsScope permissions)
        {
            Code = code;
            Permissions = permissions;
        }

        public UserPropertiesCachedViewModel(string code, List<Permission> permissions)
        {
            Code = code;
            Permissions = new PermissionsScope();
            Permissions.Permissions.AddRange(permissions.ToArray());
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}