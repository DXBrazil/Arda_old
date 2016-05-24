using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Arda.Common.Utils
{
    public class Util
    {
        public static byte[] GetBytes(string obj)
        {
            return Encoding.UTF8.GetBytes(obj);
        }

        public static string GetString(byte[] obj)
        {
            return Encoding.UTF8.GetString(obj);
        }

        public static IActionResult ConnectToRemoteService(string uri, string uniqueName, string code, string verb)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("unique_name", uniqueName);
                client.DefaultRequestHeaders.Add("code", code);

                if (verb.Equals("get"))
                {
                    return new JsonResult(client.GetStringAsync(uri));
                }
                else if (verb.Equals("post"))
                {
                    client.PostAsync(uri, null);
                    return new HttpStatusCodeResult((int)HttpStatusCode.OK);
                }
                else if (verb.Equals("put"))
                {
                    client.PutAsync(uri, null);
                    return new HttpStatusCodeResult((int)HttpStatusCode.OK);
                }
                else
                {
                    client.DeleteAsync(uri);
                    return new HttpStatusCodeResult((int)HttpStatusCode.OK);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
