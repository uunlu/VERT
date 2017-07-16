using Newtonsoft.Json.Serialization;
using Owin;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using VERT.Infrastructure;
using VERT.WebApp.StructureMapIntegration;
using System;
using System.Configuration;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.Cors;

namespace VERT.WebApp
{
    public class Startup
    {
        public static Bootstrapper bootstrapper;

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            bootstrapper = Bootstrapper.Start(new BootstrapperConfig());

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            ConfigureRoutes(config);
            ConfigureJsonFormatter(config);
            config.EnableCors();
            SwaggerConfig.Register(config);
            config.UseStructureMap(bootstrapper.RootContainer);

            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseFileServer("/wwwroot");
            appBuilder.UseWebApi(config);
        }

        private static void ConfigureJsonFormatter(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.UseDataContractJsonSerializer = true;
            jsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
        }

        private static void ConfigureRoutes(HttpConfiguration config)
        {
            config.Routes.IgnoreRoute("wwwroot", "wwwroot/{*pathInfo}");
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}