using Arda.Kanban.Interfaces;
using Arda.Kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Arda.Kanban.Repositories
{
    public class FiscalYearRepository : IFiscalYearRepository
    {
        private KanbanContext _context;

        public FiscalYearRepository(KanbanContext context)
        {
            _context = context;
        }

        // Call the respective methods to add a new fiscal year to the system.
        
        public bool AddNewFiscalYear(FiscalYear fiscalyear)
        {
            return true;
        } 
    }
}
