using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VERT.Infrastructure.Integrations;
using VERT.Services;

namespace VERT.Infrastructure
{
    /// <summary>
    /// Responsible for bootstrapping the system by configuring dependency resolution for components.
    /// </summary>
    public class Bootstrapper : IDisposable
    {
        /// <summary>
        /// Use this as a reference in delivery apps in order to create nested containers off it. DO NOT resolve dependencies directly off RootContainer.
        /// See: http://structuremap.github.io/the-container/nested-containers/
        /// </summary>
        public IContainer RootContainer { get; private set; }
        public IBootstrapperConfig Config { get; private set; }

        private Bootstrapper()
        {
        }

        /// <summary>
        /// Starts whole application.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static Bootstrapper Start(IBootstrapperConfig config)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.RootContainer = new Container();
            bootstrapper.RootContainer.Configure(x =>
            {
                x.ForSingletonOf<IBootstrapperConfig>().Use(config);

                x.AddRegistry(new MartenIntegrationRegistry(config));
                x.AddRegistry(new MediatorRegistry());
            });
            return bootstrapper;
        }

        /// <summary>
        /// Find any potential holes in your StructureMap configuration like missing dependencies, unclear defaults of plugin types, 
        /// validation errors, or just plain build errors. http://structuremap.github.io/diagnostics/validating-container-configuration/
        /// </summary>
        public void AssertStructureMapIsValid()
        {
            //var whatDoIHave = RootContainer.WhatDoIHave();
            //var whatDidIScan = RootContainer.WhatDidIScan();

            RootContainer.AssertConfigurationIsValid();
        }

        public void Dispose()
        {
            RootContainer.Dispose();
        }
    }
}
