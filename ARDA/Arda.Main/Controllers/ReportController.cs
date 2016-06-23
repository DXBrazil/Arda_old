using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Arda.Main.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetJson()
        {
            var obj = new Root()
            {
                name = "flare",
                children = new List<Child0>()
                {
                    new Child0()
                    {
                        name= "Cat1",
                        children = new List<Child>()
                        {
                            new Child()
                            {
                                name ="Design session",
                                size = 200
                            }
                        }
                    },
                    new Child0()
                    {
                        name= "Cat2",
                        children = new List<Child>()
                        {
                            new Child()
                            {
                                name ="Workshop",
                                size = 50
                            }
                        }
                    },
                    new Child0()
                    {
                        name="Cat3",
                        children = new List<Child>()
                        {
                            new Child()
                            {
                                name ="Event organization",
                                size = 70
                            }
                        }
                    },
                    new Child0()
                    {
                        name="Cat4",
                        children = new List<Child>()
                        {
                            new Child()
                            {
                                name ="Migration to the cloud",
                                size = 10
                            }
                        }
                    }
                }
            };
            return Json(obj);
        }
    }




    public class Child
    {
        public string name { get; set; }
        public int size { get; set; }
    }

    public class Child0
    {
        public string name { get; set; }
        public int? size { get; set; }
        public List<Child> children { get; set; }
    }

    public class Root
    {
        public string name { get; set; }
        public List<Child0> children { get; set; }
    }
}
