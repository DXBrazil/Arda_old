using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.ViewModels
{
    public class ResourcesViewModel
    {
        public string Category { get; set; }

        public IEnumerable<string> Resources { get; set; }

    }
}
