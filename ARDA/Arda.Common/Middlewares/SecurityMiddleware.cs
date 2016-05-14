using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Arda.Common.Middlewares
{
    public class SecurityMiddleware
    {
        RequestDelegate _next;

        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var user= context.Request.Headers["ARDAUser"].ToString();
            var code= context.Request.Headers["ARDACode"].ToString();
            var endpoint = context.Request.Host.Value;
            var resource = context.Request.Path.ToString();

            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(user))
            {
                //Bad Request:
                context.Response.StatusCode = 400;
                return;
            }
            //TODO: Comparar com o Code guardado no Redis e verificar se é válido
            else if (!CheckUserPermissionToResource(user, code, endpoint, resource))
            {
                //User doesn't have permission, code is not valid or code is expired:
                context.Response.StatusCode = 401;
                return;
            }
            else
            {
                await _next(context);
            }

        }

        private bool CheckUserPermissionToResource(string user, string code, string endpoint, string resource)
        {
            var client = new HttpClient(); ;
            client.BaseAddress = new Uri("http://localhost:2884/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("ARDAUser", user);
            client.DefaultRequestHeaders.Add("ARDACode", code);

            string url = client.BaseAddress + string.Format("values?endpoint={0}&resource={1}",endpoint,resource);
            var response = client.GetAsync(url).Result;
            var responseData = response.Content.ReadAsStringAsync().Result; // json raw data
            var permissions = JsonConvert.DeserializeObject(responseData); // json treated data

            return true;
        }
    }
}
