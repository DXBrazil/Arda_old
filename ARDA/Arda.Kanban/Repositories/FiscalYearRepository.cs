using Arda.Kanban.Interfaces;
using Arda.Kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Arda.Common.ViewModels;

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
        public List<FiscalYearMainViewModel> GetAllFiscalYears()
        {
            try
            {
                //_context.FiscalYears.OrderByDescending(fy => fy.FullNumericFiscalYear).ToList();
                var response = (from f in _context.FiscalYears
                               orderby f.FullNumericFiscalYear
                               select new FiscalYearMainViewModel
                               {
                                   FiscalYearID = f.FiscalYearID,
                                   FullNumericFiscalYearMain = f.FullNumericFiscalYear,
                                   TextualFiscalYearMain = f.TextualFiscalYear
                               }).ToList();

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

        // Return fiscal year based on ID
        public FiscalYear GetFiscalYearByID(Guid id)
        {
            try
            {
                var response = _context.FiscalYears.Where(fy => fy.FiscalYearID.Equals(id)).SingleOrDefault();

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

        // Update fiscal year data based on ID
        public bool EditFiscalYearByID(FiscalYearMainViewModel fiscalyear)
        {
            try
            {
                var fiscalYearToBeUpdated = _context.FiscalYears.SingleOrDefault(fy => fy.FiscalYearID.Equals(fiscalyear.FiscalYearID));

                if(fiscalYearToBeUpdated != null)
                {
                    // Update informations of object
                    fiscalYearToBeUpdated.FullNumericFiscalYear = fiscalyear.FullNumericFiscalYearMain;
                    fiscalYearToBeUpdated.TextualFiscalYear = fiscalyear.TextualFiscalYearMain;

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
    }
}
