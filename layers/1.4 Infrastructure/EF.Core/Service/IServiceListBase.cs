using EF.Core.Domain;
using EF.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Service
{
    public interface IServiceListBase<TEntity, TDto> : IServiceBase<TEntity>
        where TEntity : EntityBase,new()
        where TDto : IQueryDto
    {
        /// <summary>
        /// 根据Dto获取列表
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        IList<TEntity> GetListByDto(TDto queryDto);

        /// <summary>
        /// 根据Dto获取分页列表
        /// </summary>
        /// <param name="queryDto"></param>
        /// <param name="dataPage"></param>
        /// <param name="dataOrder"></param>
        /// <returns></returns>
        IList<TEntity> GetPagedListByDto(TDto queryDto, IDataPage dataPage, IDataOrder dataOrder);
    }
}
