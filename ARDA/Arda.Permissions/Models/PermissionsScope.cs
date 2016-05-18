using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.Models
{
    public class PermissionsScope
    {
        public List<Permission> Permissions { get; set; }


        public PermissionsScope()
        {
            Permissions = new List<Permission>();
        }

        public PermissionsScope(string serializedPermissions)
        {
            try
            {
                var deserializedPermissions = JsonConvert.DeserializeObject<PermissionsScope>(serializedPermissions);

                Permissions = new List<Permission>();
                foreach (var p in deserializedPermissions.Permissions)
                {
                    Permissions.Add(new Permission
                    {
                        Module = p.Module,
                        Resource = p.Resource,
                        Enabled = p.Enabled
                    });
                }
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
