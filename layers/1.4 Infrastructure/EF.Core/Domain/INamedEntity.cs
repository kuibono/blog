using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Domain
{
    public interface INamedEntity : IEntity
    {
        string Name { get; }
    }
}
