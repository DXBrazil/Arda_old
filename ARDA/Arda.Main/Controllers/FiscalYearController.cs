using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Net.Http;
using Arda.Common.Utils;
using Arda.Common.ViewModels;
using Newtonsoft.Json;
using Arda.Common.JSON;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class FiscalYearController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        // List all fiscal years in database.
        public JsonResult ListAllFiscalYears()
        {
            // Getting uniqueName
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            // Creating the final expected object to datatable
            SourceDataTablesFormat dataTablesSource = new SourceDataTablesFormat();

            // Object to host raw data about fiscal years
            List<FiscalYearMainViewModel> existentFiscalYears;

            try
            {
                // Getting the response of remote service
                var remoteServiceResponse = Util.ConnectToRemoteService<List<FiscalYearMainViewModel>>(HttpMethod.Get, Util.KanbanURL + "api/fiscalyear/list", uniqueName, "").Result;

                // Transforming raw data to FiscalYearMainViewModel format 
                existentFiscalYears = (from u in remoteServiceResponse
                                   select new FiscalYearMainViewModel
                                   {
                                       FiscalYearID = u.FiscalYearID,
                                       FullNumericFiscalYearMain = u.FullNumericFiscalYearMain,
                                       TextualFiscalYearMain = u.TextualFiscalYearMain
                                   }).ToList();

                // Mouting rows data
                foreach (FiscalYearMainViewModel fy in existentFiscalYears)
                {
                    IList<string> dataRow = new List<string>();
                    dataRow.Add(fy.TextualFiscalYearMain.ToString());
                    dataRow.Add(fy.FullNumericFiscalYearMain.ToString());
                    dataRow.Add("<a href='/fiscalyear/details/" + fy.FiscalYearID + "' class='btn btn-info'><i class='fa fa-align-justify' aria-hidden='true'></i></a>&nbsp;<a href='/fiscalyear/edit/" + fy.FiscalYearID + "' class='btn btn-info'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a>&nbsp;<a href='/fiscalyear/delete/" + fy.FiscalYearID + "' class='btn btn-info'><i class='fa fa-trash' aria-hidden='true'></i></a>");
                    dataTablesSource.aaData.Add(dataRow);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return Json(dataTablesSource);
        }

        public IActionResult Details(Guid id)
        {
            try
            {
                // Getting uniqueName
                var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

                // Getting the selected fiscal year
                var fiscalYearToBeViewed = Util.ConnectToRemoteService<FiscalYearMainViewModel>(HttpMethod.Get, Util.KanbanURL + "api/fiscalyear/getfiscalyearbyid?id=" + id, uniqueName, "").Result;

                if(fiscalYearToBeViewed != null)
                {
                    var finalFiscalYear = new FiscalYearMainViewModel()
                    {
                        FiscalYearID = fiscalYearToBeViewed.FiscalYearID,
                        FullNumericFiscalYearMain = fiscalYearToBeViewed.FullNumericFiscalYearMain,
                        TextualFiscalYearMain = fiscalYearToBeViewed.TextualFiscalYearMain
                    };

                    return View(finalFiscalYear);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
