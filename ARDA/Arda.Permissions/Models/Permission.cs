using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Permissions.Models
{
    public class Permission
    {
        public string Module { get; set; }

        public string Resource { get; set; }

        public bool Enabled { get; set; }

        public Permission() { }

        public Permission(string module, string resource, bool enabled)
        {
            Module = module;
            Resource = resource;
            Enabled = enabled;
        }
    }
}