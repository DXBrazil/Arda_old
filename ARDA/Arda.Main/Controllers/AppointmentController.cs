using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Common.Utils;
using System.Net.Http;
using Arda.Common.ViewModels.Main;
using System.Net;

namespace Arda.Main.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Add()
        {
            ViewBag.Guid = Util.GenerateNewGuid();
            return View();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddAppointment(AppointmentViewModel appointment)
        {
            if (appointment == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            var responseAboutAdd = await Util.ConnectToRemoteService(HttpMethod.Post, Util.KanbanURL + "api/appointment/add", uniqueName, "", appointment);

            if (responseAboutAdd.IsSuccessStatusCode)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
