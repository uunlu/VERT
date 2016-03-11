using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERT.Core
{
    public abstract class RootEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
