﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using Arda.Kanban.Models;
using Arda.Kanban.Interfaces;
using Arda.Common.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;

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
        [Route("addfiscalyear")]
        public HttpResponseMessage Add()
        {
            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.Body);
                string requestFromPost = reader.ReadToEnd();
                var fiscalYear = JsonConvert.DeserializeObject<FiscalYearMainViewModel>(requestFromPost);

                // Calling update
                var fiscalyear = _repository.AddNewFiscalYear(fiscalYear);

                if (fiscalyear)
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

        [HttpPut]
        [Route("editfiscalyearbyid")]
        public IActionResult EditFiscalYearByID()
        {
            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.Body);
                string requestFromPost = reader.ReadToEnd();
                var fiscalYear = JsonConvert.DeserializeObject<FiscalYearMainViewModel>(requestFromPost);

                // Calling update
                var fiscalyear = _repository.EditFiscalYearByID(fiscalYear);

                if (fiscalyear)
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
        [Route("deletefiscalyearbyid")]
        public HttpResponseMessage DeleteFiscalYearByID(Guid id)
        {
            try
            {
                var response = _repository.DeleteFiscalYearByID(id);

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
