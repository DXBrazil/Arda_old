using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authentication.OpenIdConnect;

namespace Arda.Main
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("secrets.json")
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
            });

            app.UseOpenIdConnectAuthentication(options =>
            {
                options.AutomaticChallenge = true;
                options.ClientId = Configuration["Authentication:AzureAd:ClientId"];
                options.Authority = Configuration["Authentication:AzureAd:AADInstance"] + Configuration["Authentication:AzureAd:TenantId"];
                options.PostLogoutRedirectUri = Configuration["Authentication:AzureAd:PostLogoutRedirectUri"];
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Events = new OpenIdConnectEvents()
                {
                    OnAuthorizationCodeReceived = (context) =>
                    {
                        var claims = context.JwtSecurityToken.Claims;

                        var authCode = context.Code;
                        var validFrom = context.JwtSecurityToken.ValidFrom;
                        var validTo = context.JwtSecurityToken.ValidTo;
                        var givenName = claims.FirstOrDefault(claim => claim.Type == "given_name").Value;
                        var name = claims.FirstOrDefault(claim => claim.Type == "name").Value;
                        var uniqueName = claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;

                        return Task.FromResult(0);
                    }
                };
            });

            //app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            //{
            //    ClientId = Configuration["Authentication:AzureAd:ClientId"],
            //    Authority = Configuration["Authentication:AzureAd:AADInstance"] + Configuration["Authentication:AzureAd:TenantId"],
            //    PostLogoutRedirectUri = Configuration["Authentication:AzureAd:PostLogoutRedirectUri"],
            //    SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme,
            //    Events = new OpenIdConnectEvents()
            //    {
            //        OnAuthenticationValidated =(context) =>
            //        {

            //            return Task.FromResult(0);
            //        },
            //        OnUserInformationReceived = (context) =>
            //        {

            //            return Task.FromResult(0);
            //        },
            //        OnAuthorizationCodeReceived = (context) =>
            //        {

            //            return Task.FromResult(0);
            //        },
            //        OnAuthorizationResponseReceived = (context) =>
            //        {

            //            return Task.FromResult(0);
            //        }
            //    }
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
