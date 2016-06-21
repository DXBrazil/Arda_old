using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Arda.Common.Utils;
using System.Net.Http;
using Arda.Common.ViewModels.Main;
using System.Net;
using Arda.Common.JSON;

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

            // Converting the T&E value to Decimal before save process
            appointment._AppointmentTE = Decimal.Parse(Request.Form["_AppointmentTE"]);

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

        public IActionResult My()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ListAllAppointments()
        {
            var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            SourceDataTablesFormat dataTablesSource = new SourceDataTablesFormat();

            try
            {
                var existentAppointments = await Util.ConnectToRemoteService<List<AppointmentViewModel>>(HttpMethod.Get, Util.KanbanURL + "api/appointment/list", uniqueName, "");

                foreach (AppointmentViewModel m in existentAppointments)
                {
                    IList<string> dataRow = new List<string>();
                    dataRow.Add(m._WorkloadTitle.ToString());
                    dataRow.Add(m._AppointmentDate.ToString("dd/MM/yyyy"));
                    dataRow.Add(m._AppointmentHoursDispensed.ToString());
                    dataRow.Add(Util.GetUserAlias(m._AppointmentUserUniqueName.ToString()));
                    dataRow.Add($"<div class='data-sorting-buttons'><a href='/appointment/details/{m._AppointmentID}' class='ds-button-detail'><i class='fa fa-align-justify' aria-hidden='true'></i> Details</a></div>&nbsp;<div class='data-sorting-buttons'><a href='#' onclick=\"loadWorkload('{m._AppointmentWorkloadWBID}');\" data-toggle='modal' data-target='#WorkloadModal' class='ds-button-edit'><i class='fa fa-tasks' aria-hidden='true'></i> Workload</a></div>&nbsp;<div class='data-sorting-buttons'><a data-toggle='modal' data-target='#generic-modal' onclick=\"ModalDelete_Appointment('{m._AppointmentID}','{m._WorkloadTitle}','{m._AppointmentDate.ToString("dd/MM/yyyy")}','{m._AppointmentHoursDispensed}','{m._AppointmentUserUniqueName}');\" class='ds-button-delete'><i class='fa fa-trash' aria-hidden='true'></i> Delete</a></div>");
                    dataTablesSource.aaData.Add(dataRow);
                }
            }
            catch (Exception)
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
                var appointmentToBeViewed = Util.ConnectToRemoteService<AppointmentViewModel>(HttpMethod.Get, Util.KanbanURL + "api/appointment/getappointmentbyid?id=" + id, uniqueName, "").Result;

                if (appointmentToBeViewed != null)
                {
                    return View(appointmentToBeViewed);
                }
                else
                {
                    ViewBag.Message = "The system has not found the requested appointment.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAppointment(Guid id)
        {
            try
            {
                var uniqueName = HttpContext.User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

                var appointmentToBeDeleted = await Util.ConnectToRemoteService(HttpMethod.Delete, Util.KanbanURL + "api/appointment/deleteappointmentbyid?id=" + id, uniqueName, "", id);

                if (appointmentToBeDeleted.IsSuccessStatusCode)
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
