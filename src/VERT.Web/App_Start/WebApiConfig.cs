using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace VERT.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Routes.IgnoreRoute("storage", "storage/{*pathInfo}");
            config.Routes.IgnoreRoute("scripts", "scripts/{*pathInfo}");
            config.Routes.IgnoreRoute("styles", "styles /{*pathInfo}");
            config.Routes.IgnoreRoute("imgs", "imgs/{*pathInfo}");
            config.Routes.IgnoreRoute("fonts", "fonts/{*pathInfo}");

            config.MapHttpAttributeRoutes();

            // Route to index.html
            config.Routes.MapHttpRoute(
                name: "Index",
                routeTemplate: "{id}.html",
                defaults: new { id = "index" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
