using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.Models
{
    public class Permission
    {
        public string Module { get; set; }

        public bool Enabled { get; set; }

        public List<Permission> NestedPermissions { get; set; }


        public Permission() { }

        public Permission(string module, bool enabled)
        {
            Module = module;
            Enabled = enabled;
        }

        public Permission(string serializedPermission)
        {
            try
            {
                var permission = JsonConvert.DeserializeObject<Permission>(serializedPermission);

                Module = permission.Module;
                Enabled = permission.Enabled;
                NestedPermissions = permission.NestedPermissions;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public override string ToString()
        {
            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}