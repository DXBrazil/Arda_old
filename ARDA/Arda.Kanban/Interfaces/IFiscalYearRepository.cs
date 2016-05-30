using Arda.Common.ViewModels;
using Arda.Kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Kanban.Interfaces
{
    public interface IFiscalYearRepository
    {
        // Add a new fiscal year to the database.
        bool AddNewFiscalYear(FiscalYear fiscalyear);

        // Return a list of fiscal years.
        List<FiscalYearMainViewModel> GetAllFiscalYears();

        // Return a specific fiscal year by ID.
        FiscalYear GetFiscalYearByID(Guid id);
    }
}
