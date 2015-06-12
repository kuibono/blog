using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Domain
{
    public class NamedEntityBase:EntityBase
    {
        public virtual string Name { get; set; }
    }
}
