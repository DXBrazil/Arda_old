﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Common.Utils;
using Arda.Common.ViewModels.Main;
using System.Net.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Main.Controllers
{
    public class ActivityController : Controller
    {
        [HttpGet]
        public async Task<JsonResult> GetActivities()
        {
            // Getting uniqueName
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            try
            {
                // Getting the response of remote service
                var activity = await Util.ConnectToRemoteService<List<ActivityViewModel>>(HttpMethod.Get, Util.KanbanURL + "api/activity/list", uniqueName, "");

                return Json(activity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
