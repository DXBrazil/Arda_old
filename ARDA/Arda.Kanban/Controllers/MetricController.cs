using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Kanban.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Arda.Common.ViewModels;

namespace Arda.Kanban.Controllers
{
    [Route("api/[controller]")]
    public class MetricController : Controller
    {
        IMetricRepository _repository;

        public MetricController(IMetricRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add()
        {
            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.Body);
                string requestFromPost = reader.ReadToEnd();
                var metric = JsonConvert.DeserializeObject<MetricMainViewModel>(requestFromPost);

                // Calling update
                var response = _repository.AddNewMetric(metric);

                if (response)
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

        [HttpGet]
        [Route("list")]
        public IEnumerable<MetricMainViewModel> List()
        {
            try
            {
                var metrics = _repository.GetAllMetrics();

                if (metrics != null)
                {
                    return metrics;
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
        [Route("getmetricbyid")]
        public MetricMainViewModel GetMetricByID(Guid id)
        {
            try
            {
                var metric = _repository.GetMetricByID(id);

                if (metric != null)
                {
                    return new MetricMainViewModel()
                    {
                        MetricID = metric.MetricID,
                        MetricCategory = metric.MetricCategory,
                        MetricName = metric.MetricName,
                        Description = metric.Description,
                        FiscalYearID = metric.FiscalYear.FiscalYearID,
                        FullNumericFiscalYear = metric.FiscalYear.FullNumericFiscalYear,
                        TextualFiscalYear = metric.FiscalYear.TextualFiscalYear
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

        [HttpPut]
        [Route("editmetricbyid")]
        public IActionResult EditMetricByID()
        {
            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.Body);
                string requestFromPost = reader.ReadToEnd();
                var metric = JsonConvert.DeserializeObject<MetricMainViewModel>(requestFromPost);

                // Calling update
                var response = _repository.EditMetricByID(metric);

                if (response)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.OK);
                }
                else
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("deletemetricbyid")]
        public HttpResponseMessage DeleteMetricByID(Guid id)
        {
            try
            {
                var response = _repository.DeleteMetricByID(id);

                if (response)
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
    }
}
