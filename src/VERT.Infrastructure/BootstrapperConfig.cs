using System.Configuration;
using VERT.Services;

namespace VERT.Infrastructure
{
    public class BootstrapperConfig : IBootstrapperConfig
    {
        public string Database_ConnString => ConfigurationManager.AppSettings[nameof(Database_ConnString)];
    }
}
