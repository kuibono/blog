using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Dto
{
    /// <summary>
    /// <para>返回错误或者数据Dto</para>
    /// <para>主要用于返回数据前做校验返回错误类型等信息</para>
    /// </summary>
    /// <typeparam name="TError">错误类型,必须为值类型</typeparam>
    /// <typeparam name="TData">数据类型</typeparam>
    public class ErrorOrDataDto<TError, TData> where TError : struct
    {
        /// <summary>
        /// 错误
        /// </summary>
        public TError? Error { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; }
    }
}
