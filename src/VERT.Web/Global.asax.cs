using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using VERT.Infrastructure;
using VERT.Web.StructureMapIntegration;
using Newtonsoft.Json.Serialization;

namespace VERT.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var configJsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            configJsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            configJsonFormatter.UseDataContractJsonSerializer = true;

            var bootstrapper = Bootstrapper.Start(new BootstrapperConfig());
            GlobalConfiguration.Configuration.UseStructureMap(bootstrapper.RootContainer);
        }
    }
}
