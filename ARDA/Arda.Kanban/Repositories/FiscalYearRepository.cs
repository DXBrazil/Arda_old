using Arda.Kanban.Interfaces;
using Arda.Common.Kanban.Models;
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
        public bool AddNewFiscalYear(FiscalYearMainViewModel fiscalyear)
        {
            try
            {
                var fiscalYearToBeSaved = new FiscalYear()
                {
                    FiscalYearID = fiscalyear.FiscalYearID,
                    FullNumericFiscalYear = fiscalyear.FullNumericFiscalYearMain,
                    TextualFiscalYear = fiscalyear.TextualFiscalYearMain
                };

                _context.FiscalYears.Add(fiscalYearToBeSaved);
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
        public FiscalYearMainViewModel GetFiscalYearByID(Guid id)
        {
            try
            {
                var response = _context.FiscalYears.Where(fy => fy.FiscalYearID.Equals(id)).SingleOrDefault();

                var fiscalYear = new FiscalYearMainViewModel()
                {
                    FiscalYearID = response.FiscalYearID,
                    TextualFiscalYearMain = response.TextualFiscalYear,
                    FullNumericFiscalYearMain = response.FullNumericFiscalYear
                };

                if (fiscalYear != null)
                {
                    return fiscalYear;
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

                if (fiscalYearToBeUpdated != null)
                {
                    // Update informations of object
                    fiscalYearToBeUpdated.FullNumericFiscalYear = fiscalyear.FullNumericFiscalYearMain;
                    fiscalYearToBeUpdated.TextualFiscalYear = fiscalyear.TextualFiscalYearMain;

                    var response = _context.SaveChanges();

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

        // Delete fiscal year based on ID
        public bool DeleteFiscalYearByID(Guid id)
        {
            try
            {
                var fiscalYearToBeDeleted = _context.FiscalYears.SingleOrDefault(fy => fy.FiscalYearID.Equals(id));

                if (fiscalYearToBeDeleted != null)
                {
                    var response = _context.Remove(fiscalYearToBeDeleted);
                    _context.SaveChanges();

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
    }
}
