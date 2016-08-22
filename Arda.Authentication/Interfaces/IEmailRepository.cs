using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Authentication.Interfaces
{
    public interface IEmailRepository
    {
        bool SendEmailRequestNewAccount(string Name, string Email, string Phone, string Message);

        bool SendHelpRequest(string RequestType);
    }
}
