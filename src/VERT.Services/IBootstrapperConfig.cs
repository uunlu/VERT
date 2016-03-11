using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERT.Services
{
    public interface IBootstrapperConfig
    {
        string Database_ConnString { get; }
    }
}
