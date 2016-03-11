using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VERT.Infrastructure;

namespace VERT.Testing.IntegrationTests
{
    public class BootstrapperFixture : IDisposable
    {
        protected Bootstrapper boostrapper;

        public BootstrapperFixture()
        {
            if (boostrapper == null)
            {
                boostrapper = Bootstrapper.Start(new BootstrapperConfig());
            }
        }

        public void Dispose()
        {
            boostrapper.Dispose();
        }
    }
}
