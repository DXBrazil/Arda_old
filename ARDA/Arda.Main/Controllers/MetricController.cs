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
        public JsonResult ListAllMetrics()
        {
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            SourceDataTablesFormat dataTablesSource = new SourceDataTablesFormat();

            try
            {
                var existentMetrics = Util.ConnectToRemoteService<List<MetricMainViewModel>>(HttpMethod.Get, Util.KanbanURL + "api/metric/list", uniqueName, "").Result;

                foreach (MetricMainViewModel m in existentMetrics)
                {
                    IList<string> dataRow = new List<string>();
                    dataRow.Add(m.TextualFiscalYear.ToString());
                    dataRow.Add(m.MetricCategory.ToString());
                    dataRow.Add(m.MetricName.ToString());
                    dataRow.Add($"<a href='/metric/details/{m.MetricID}' class='btn btn-info'><i class='fa fa-align-justify' aria-hidden='true'></i></a>&nbsp;<a href='/metric/edit/{m.MetricID}' class='btn btn-info'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a>&nbsp;<a href='javascript:void()' data-toggle='modal' data-target='#generic-modal' onclick=\"ModalDelete_Metric('{m.MetricID}','{m.MetricCategory}','{m.MetricName}');\" class='btn btn-info'><i class='fa fa-trash' aria-hidden='true'></i></a>");
                    dataTablesSource.aaData.Add(dataRow);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Json(dataTablesSource);
        }

        [HttpGet]
        public JsonResult ListAllCategories()
        {
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            var categories = new List<string>();

            try
            {
                var existentMetrics = Util.ConnectToRemoteService<List<MetricMainViewModel>>(HttpMethod.Get, Util.KanbanURL + "api/metric/list", uniqueName, "").Result;

                foreach (MetricMainViewModel m in existentMetrics)
                {
                    if (!categories.Contains(m.MetricCategory))
                    {
                        categories.Add(m.MetricCategory);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Json(categories);
        }

        [HttpPost]
        public HttpResponseMessage AddMetric(MetricMainViewModel metric)
        {
            if (metric == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            var responseAboutUpdate = Util.ConnectToRemoteService(HttpMethod.Post, Util.KanbanURL + "api/metric/add", uniqueName, "", metric).Result;

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
