using Arda.Common.ViewModel;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
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

        public static T ConnectToRemoteService<T>(string uri, string uniqueName, string code, string verb)
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
                    var responseRaw = client.GetAsync(uri).Result;
                    var responseJson = responseRaw.Content.ReadAsStringAsync().Result;
                    var responseConverted = JsonConvert.DeserializeObject<T>(responseJson);
                    return responseConverted;
                }
                else if (verb.Equals("post"))
                {
                    var responseRaw = client.PostAsync(uri, null).Result;
                    var responseJson = responseRaw.Content.ReadAsStringAsync().Result;
                    var responseConverted = JsonConvert.DeserializeObject<T>(responseJson);
                    return responseConverted;
                }
                else if (verb.Equals("put"))
                {
                    var responseRaw = client.PutAsync(uri, null).Result;
                    var responseJson = responseRaw.Content.ReadAsStringAsync().Result;
                    var responseConverted = JsonConvert.DeserializeObject<T>(responseJson);
                    return responseConverted;
                }
                else
                {
                    var responseRaw = client.DeleteAsync(uri).Result;
                    var responseJson = responseRaw.Content.ReadAsStringAsync().Result;
                    var responseConverted = JsonConvert.DeserializeObject<T>(responseJson);
                    return responseConverted;
                }
            }
            catch (Exception e)
            {
                throw;
            }
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
                    client.GetAsync(uri);
                    return new HttpStatusCodeResult((int)HttpStatusCode.OK);
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
