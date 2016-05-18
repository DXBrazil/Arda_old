using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Reports.Controllers
{
    [Route("api/[controller]")]
    public class InfosController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("getinfo")]
        public IEnumerable<string> GetInfo()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
