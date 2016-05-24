using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.ViewModel
{

    public class FiscalYearViewModel
    {
        public Guid FiscalYearID { get; set; }

        public int FullNumericFiscalYear { get; set; }

        public string TextualFiscalYear { get; set; }
    }
}
