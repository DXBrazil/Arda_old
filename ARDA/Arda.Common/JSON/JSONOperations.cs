using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace Arda.Common.JSON
{
    public class JSONOperations
    {
        // Get a string formated with JSON based in any structured object.
        public static string GetJsonResult(object objectToBeConverted)
        {
            return JsonConvert.SerializeObject(objectToBeConverted).ToString();
        }
    }
}
