using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using Arda.Kanban.Models;
using Arda.Kanban.Interfaces;
using Arda.Common.ViewModels;

namespace Arda.Kanban.Controllers
{
    [Route("api/[controller]")]
    public class FiscalYearController : Controller
    {
        private IFiscalYearRepository _repository;

        public FiscalYearController(IFiscalYearRepository repository)
        {
            _repository = repository;
        }
         
        [HttpPost]
        [Route("add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("FullNumericFiscalYear, TextualFiscalYear")] FiscalYear fiscalYear)
        {
            try
            {
                if(fiscalYear != null)
                {
                    var response = _repository.AddNewFiscalYear(fiscalYear);

                    if(response)
                    {
                        return new HttpStatusCodeResult((int)HttpStatusCode.OK);
                    }
                    else
                    {
                        return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                    }
                }
                else
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }            
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<FiscalYearMainViewModel> List()
        {
            try
            {
                var fiscalyears = _repository.GetAllFiscalYears();

                if (fiscalyears != null)
                {
                    return fiscalyears;
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

        [HttpGet]
        [Route("getfiscalyearbyid")]
        public FiscalYearMainViewModel GetFiscalYearByID(Guid id)
        {
            try
            {
                var fiscalYear = _repository.GetFiscalYearByID(id);

                if(fiscalYear != null)
                {
                    return new FiscalYearMainViewModel() {
                         FiscalYearID = fiscalYear.FiscalYearID,
                         TextualFiscalYearMain = fiscalYear.TextualFiscalYear,
                         FullNumericFiscalYearMain = fiscalYear.FullNumericFiscalYear
                    };
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
