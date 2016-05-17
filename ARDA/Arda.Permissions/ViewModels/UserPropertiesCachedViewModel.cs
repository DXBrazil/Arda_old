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

        public Permission Permissions { get; set; }

        public UserPropertiesCachedViewModel() { }

        public UserPropertiesCachedViewModel(string propertiesCachedSerialized)
        {
            var prop = JsonConvert.DeserializeObject<UserPropertiesCachedViewModel>(propertiesCachedSerialized);
            Code = prop.Code;
            Permissions = prop.Permissions;
        }

        public UserPropertiesCachedViewModel(Permission permissions)
        {
            Code = "";
            Permissions = permissions;
        }

        public UserPropertiesCachedViewModel(string code, Permission permissions)
        {
            Code = code;
            Permissions = permissions;
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}