using StructureMap;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace VERT.Web.StructureMapIntegration
{
    public class WebApiFilterProviderStructureMap : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IContainer _container;

        public WebApiFilterProviderStructureMap(IContainer container)
        {
            _container = container;
        }

        public new IEnumerable<FilterInfo> GetFilters(
            HttpConfiguration configuration,
            HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);

            foreach (var filter in filters)
            {
                _container.BuildUp(filter.Instance);
            }

            return filters;
        }
    }
}
