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
        public static readonly string KanbanURL = "http://localhost:2768/";
        public static readonly string MainURL = "https://localhost:44304/";
        public static readonly string ReportsURL = "http://localhost:2891/";
        public static readonly string PermissionsURL = "http://localhost:2884/";

        public static byte[] GetBytes(string obj)
        {
            return Encoding.UTF8.GetBytes(obj);
        }

        public static string GetString(byte[] obj)
        {
            return Encoding.UTF8.GetString(obj);
        }

        public static async Task<T> ConnectToRemoteService<T>(HttpMethod method, string url, string uniqueName, string code)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(method, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                request.Headers.Add("unique_name", uniqueName);
                request.Headers.Add("code", code);

                var responseRaw = await client.SendAsync(request);
                var responseJson = responseRaw.Content.ReadAsStringAsync().Result;
                var responseConverted = JsonConvert.DeserializeObject<T>(responseJson);

                return responseConverted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<HttpResponseMessage> ConnectToRemoteService(HttpMethod method, string url, string uniqueName, string code)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(method, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("unique_name", uniqueName);
                request.Headers.Add("code", code);

                var responseSend = await client.SendAsync(request);
                var responseStr = await responseSend.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<HttpResponseMessage>(responseStr);

                if (responseSend.IsSuccessStatusCode && responseObj.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public static async Task<HttpResponseMessage> ConnectToRemoteService<T>(HttpMethod method, string url, string uniqueName, string code, T body)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(method, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("unique_name", uniqueName);
                request.Headers.Add("code", code);

                var serialized = JsonConvert.SerializeObject(body);
                request.Content = new ByteArrayContent(GetBytes(serialized));

                var responseSend = await client.SendAsync(request);
                var responseStr = await responseSend.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<HttpResponseMessage>(responseStr);

                if (responseSend.IsSuccessStatusCode && responseObj.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public static async Task<HttpResponseMessage> ConnectToRemoteService<T>(HttpMethod method, string url, T body)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(method, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (body != null)
                {
                    var serialized = JsonConvert.SerializeObject(body);
                    request.Content = new ByteArrayContent(GetBytes(serialized));
                }

                var responseSend = await client.SendAsync(request);
                var responseStr = await responseSend.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<HttpResponseMessage>(responseStr);

                if (responseSend.IsSuccessStatusCode && responseObj.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public static async Task<HttpResponseMessage> ConnectToRemoteService<T>(HttpMethod method, string url, string uniqueName, string code, Guid id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(method, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("unique_name", uniqueName);
                request.Headers.Add("code", code);

                var responseSend = await client.SendAsync(request);
                var responseStr = await responseSend.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<HttpResponseMessage>(responseStr);

                if (responseSend.IsSuccessStatusCode && responseObj.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public static Guid GenerateNewGuid()
        {
            return Guid.NewGuid();
        }
    }
}