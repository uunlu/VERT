using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERT.Core
{
    public abstract class RootEntity
    {
        public RootEntity()
        {
            CreatedOnUtc = DateTime.UtcNow;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedOnUtc { get; private set; }
    }
}
