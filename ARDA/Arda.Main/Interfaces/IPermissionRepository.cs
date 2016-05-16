using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Main.Interfaces
{
    public interface IPermissionRepository
    {
        bool WriteTokenAndUserPermissions(string uniqueName, string authorizationCode);

    }
}
