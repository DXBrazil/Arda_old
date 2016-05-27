using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authentication.OpenIdConnect;
using Microsoft.AspNet.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Arda.Common.Utils;
using Arda.Main.Utils;

namespace Arda.Main
{
    public partial class Startup
    {
        public static string Authority = string.Empty;
        public static string CallbackPath = string.Empty;
        public static string ClientId = string.Empty;
        public static string ClientSecret = string.Empty;
        public static string GraphResourceId = string.Empty;
        public static string PostLogoutRedirectUri = string.Empty;

        public void ConfigureAuth(IApplicationBuilder app)
        {
            // Populate Azure AD Configuration Values
            Authority = Configuration["Authentication:AzureAd:AADInstance"] + Configuration["Authentication:AzureAd:TenantId"];
            CallbackPath = Configuration["Authentication:AzureAd:CallbackPath"];
            ClientId = Configuration["Authentication:AzureAd:ClientId"];
            ClientSecret = Configuration["Authentication:AzureAd:ClientSecret"];
            GraphResourceId = Configuration["Authentication:AzureAd:GraphResourceId"];
            PostLogoutRedirectUri = Configuration["Authentication:AzureAd:PostLogoutRedirectUri"];

            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
            });

            app.UseOpenIdConnectAuthentication(options =>
            {
                options.AutomaticChallenge = true;
                options.CallbackPath = new PathString(CallbackPath);
                options.ClientId = ClientId;
                options.Authority = Authority;
                options.PostLogoutRedirectUri = PostLogoutRedirectUri;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Events = new OpenIdConnectEvents()
                {
                    OnAuthenticationFailed = OnAuthenticationFailed,
                    OnAuthorizationCodeReceived = OnAuthorizationCodeReceived
                };
            });
        }

        public Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            Debugger.Break();
            context.HandleResponse();
            context.Response.Redirect("/Home/Error?message=" + context.Exception.Message);

            return Task.FromResult(0);
        }

        public async Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedContext context)
        {
            CacheUserAndCodeOnRedis(context);
            await AcquireTokenForMicrosoftGraph(context);
        }

        private void CacheUserAndCodeOnRedis(AuthorizationCodeReceivedContext context)
        {
            var claims = context.JwtSecurityToken.Claims;

            // Getting informations about AD
            var code = context.Code;
            var validFrom = context.JwtSecurityToken.ValidFrom;
            var validTo = context.JwtSecurityToken.ValidTo;
            var givenName = claims.FirstOrDefault(claim => claim.Type == "given_name").Value;
            var name = claims.FirstOrDefault(claim => claim.Type == "name").Value;
            var uniqueName = claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;

            Util.ConnectToRemoteService("http://localhost:2884/api/permission/setuserpermissionsandcode", uniqueName, code, "post");
        }

        private async Task AcquireTokenForMicrosoftGraph(AuthorizationCodeReceivedContext context)
        {
            // Acquire a Token for the Graph API and cache it in Session.
            string userObjectId = context.AuthenticationTicket.Principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            ClientCredential clientCred = new ClientCredential(ClientId, ClientSecret);
            AuthenticationContext authContext = new AuthenticationContext(Authority, new SessionCache(userObjectId, context.HttpContext));
            AuthenticationResult authResult = await authContext.AcquireTokenByAuthorizationCodeAsync(
                context.Code, new Uri(context.RedirectUri), clientCred, GraphResourceId);
        }

    }
}
