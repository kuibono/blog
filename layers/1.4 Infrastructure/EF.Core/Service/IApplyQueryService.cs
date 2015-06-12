using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Service
{
    public interface IApplyQueryService
    {
        /// <summary>
        /// 根据指定的代理方法查询并返回
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="applyFunc"></param>
        /// <returns></returns>
        TResult ApplyQuery<TResult>(Func<IQueryable, TResult> applyFunc, object queryParams = null);
    }
}
