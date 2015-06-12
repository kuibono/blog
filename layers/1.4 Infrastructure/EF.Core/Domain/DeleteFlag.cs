using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Domain
{
    /// <summary>
    /// 删除标记
    /// </summary>
    public enum DeleteFlag
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 1
    }
}
