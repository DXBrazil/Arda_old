using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using Arda.Kanban.Models;
using Arda.Kanban.Interfaces;

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
        public IEnumerable<FiscalYear> List()
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
    }
}
