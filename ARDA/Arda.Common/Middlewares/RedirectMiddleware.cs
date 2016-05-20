using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Arda.Common.Middlewares
{
    public class RedirectMiddleware
    {
        RequestDelegate _next;
        HttpResponseMessage _message;

        public RedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_message.StatusCode != HttpStatusCode.OK)
            {
                //context.Response.StatusCode = 401; //Unauthorized
                context.Response.Redirect("/Errors/NotReady");
                return;
            }

            await _next.Invoke(context);
        }
    }
}
