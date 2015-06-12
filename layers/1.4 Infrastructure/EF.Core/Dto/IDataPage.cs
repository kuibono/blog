using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Dto
{
    public interface IDataPage
    {
        /// <summary>
        /// 跳过条数
        /// </summary>
        int Skip { get; }

        /// <summary>
        /// 取得条数
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        int RecordCount { get; set; }
    }
}
