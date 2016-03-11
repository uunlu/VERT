using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using VERT.Infrastructure;

namespace VERT.Testing.IntegrationTests
{
    public class BootstrappingTests
    {
        [Fact]
        public void start()
        {
            var bootstrapper = Bootstrapper.Start(new BootstrapperConfig());

            bootstrapper.ShouldNotBeNull();
            bootstrapper.AssertStructureMapIsValid();
        }
    }
}
