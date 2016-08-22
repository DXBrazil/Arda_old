using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Authentication.ViewModels.AccountOperations
{
    public class RequestNewAccountViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Justification { get; set; }
    }
}
