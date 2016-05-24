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

        // Adds a new fiscal year to the system.
        public bool AddNewFiscalYear(FiscalYear fiscalyear)
        {
            try
            {
                _context.FiscalYears.Add(fiscalyear);
                var response = _context.SaveChanges();

                if (response > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        } 

        // Return a 'numberOfOccurencies' to controller.
        public List<FiscalYear> GetFiscalYearsByNumberOfOccurency(int numberOfOccurencies)
        {
            try
            {
                var response = _context.FiscalYears.OrderByDescending(fy => fy.FullNumericFiscalYear).Take(numberOfOccurencies).ToList();

                if (response != null)
                {
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
