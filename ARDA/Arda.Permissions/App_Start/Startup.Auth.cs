using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Arda.Common.Utils;
using System.Net.Http;

namespace Arda.Permissions
{
    public partial class Startup
    {
        public static string Authority = string.Empty;
        public static string Audience = string.Empty;

        public void ConfigureAuth(IApplicationBuilder app)
        {
            // Populate Azure AD Configuration Values
            Authority = Configuration["Authentication:AzureAd:AADInstance"] + Configuration["Authentication:AzureAd:TenantId"];
            Audience = Configuration["Authentication:AzureAd:Permissions:Audience"];

            // Configure the app to use Jwt Bearer Authentication
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Authority = Authority,
                Audience = Audience,
            });
        }
    }
}