using Arda.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Authentication.Interfaces
{
    public interface IAuthentication
    {
        User GetUserByEmailAndPassword(string email, string password);
    }
}
