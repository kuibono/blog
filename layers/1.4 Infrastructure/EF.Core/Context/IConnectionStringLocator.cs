using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Context
{
    public interface IConnectionStringLocator
    {
        string ConnectionString { get; }
    }
}
