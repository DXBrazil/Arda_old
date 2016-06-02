using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Main.Infra;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNet.Authorization;
using Newtonsoft.Json;
using Arda.Common.JSON;
using Arda.Common.Utils;
using Arda.Common.ViewModels;
using System.Net;

namespace Arda.Main.Controllers
{
    [Authorize]
    public class MetricController : Controller
    {

        #region Views

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            ViewBag.Guid = Util.GenerateNewGuid().ToString();
            return View();
        }

        public IActionResult Details(Guid id)
        {
            try
            {
                // Getting uniqueName
                var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

                // Getting the selected fiscal year
                var fiscalYearToBeViewed = Util.ConnectToRemoteService<FiscalYearMainViewModel>(HttpMethod.Get, Util.KanbanURL + "api/fiscalyear/getfiscalyearbyid?id=" + id, uniqueName, "").Result;

                if (fiscalYearToBeViewed != null)
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
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        public IActionResult Edit(Guid id)
        {
            try
            {
                // Getting uniqueName
                var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

                // Getting the selected fiscal year
                var fiscalYearToBeViewed = Util.ConnectToRemoteService<FiscalYearMainViewModel>(HttpMethod.Get, Util.KanbanURL + "api/fiscalyear/getfiscalyearbyid?id=" + id, uniqueName, "").Result;

                if (fiscalYearToBeViewed != null)
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
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        #endregion


        #region Actions

        [HttpGet]
        public JsonResult ListAllFiscalYears()
        {
            // Getting uniqueName
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            // Creating the final expected object to datatable
            SourceDataTablesFormat dataTablesSource = new SourceDataTablesFormat();

            try
            {
                // Getting the response of remote service
                var existentFiscalYears = Util.ConnectToRemoteService<List<FiscalYearMainViewModel>>(HttpMethod.Get, Util.KanbanURL + "api/fiscalyear/list", uniqueName, "").Result;

                // Mouting rows data
                foreach (FiscalYearMainViewModel fy in existentFiscalYears)
                {
                    IList<string> dataRow = new List<string>();
                    dataRow.Add(fy.TextualFiscalYearMain.ToString());
                    dataRow.Add(fy.FullNumericFiscalYearMain.ToString());
                    dataRow.Add($"<a href='/fiscalyear/details/{fy.FiscalYearID}' class='btn btn-info'><i class='fa fa-align-justify' aria-hidden='true'></i></a>&nbsp;<a href='/fiscalyear/edit/{fy.FiscalYearID}' class='btn btn-info'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a>&nbsp;<a href='javascript:void()' data-toggle='modal' data-target='#generic-modal' onclick=\"ModalDelete_FiscalYear('{fy.FiscalYearID}','{fy.TextualFiscalYearMain}');\" class='btn btn-info'><i class='fa fa-trash' aria-hidden='true'></i></a>");
                    dataTablesSource.aaData.Add(dataRow);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Json(dataTablesSource);
        }

        [HttpPost]
        public HttpResponseMessage AddMetric(MetricMainViewModel fiscalYear)
        {
            if (fiscalYear == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            // Getting uniqueName
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            // Getting the selected fiscal year
            var responseAboutUpdate = Util.ConnectToRemoteService(HttpMethod.Post, Util.KanbanURL + "api/fiscalyear/addfiscalyear", uniqueName, "", fiscalYear).Result;

            if (responseAboutUpdate.IsSuccessStatusCode)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage EditFiscalYear(MetricMainViewModel fiscalyear)
        {
            if (fiscalyear == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            // Getting uniqueName
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            // Getting the selected fiscal year
            var responseAboutUpdate = Util.ConnectToRemoteService(HttpMethod.Put, Util.KanbanURL + "api/fiscalyear/editfiscalyearbyid", uniqueName, "", fiscalyear).Result;

            if (responseAboutUpdate.IsSuccessStatusCode)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteMetric(Guid id)
        {
            try
            {
                // Getting uniqueName
                var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

                // Getting the selected fiscal year
                var fiscalYearToBeDeleted = Util.ConnectToRemoteService(HttpMethod.Delete, Util.KanbanURL + "api/fiscalyear/deletefiscalyearbyid?id=" + id, uniqueName, "", id).Result;

                if (fiscalYearToBeDeleted.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        #endregion


        #region Utils


        #endregion

    }
}
