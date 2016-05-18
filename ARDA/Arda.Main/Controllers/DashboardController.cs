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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Main.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        HttpClient client;

        public DashboardController()
        {
            //client = new HttpClient();

            ////Reports
            //client.BaseAddress = new Uri("http://localhost:2891/api/");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ////TODO: Change for the session values
            //client.DefaultRequestHeaders.Add("unique_name", "hue123");
            //client.DefaultRequestHeaders.Add("ARDAUser", "huehue");
        }

        public IActionResult Index()
        {
            //string url = client.BaseAddress + "values";
            //var response = client.GetAsync(url).Result;
            //var responseData = response.Content.ReadAsStringAsync().Result; // json raw data
            //var permissions = JsonConvert.DeserializeObject(responseData); // json treated data
            return View();
        }
    }
}
