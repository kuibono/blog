using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EF.Core.Domain;
using EF.Core.Dto;
using EF.Core.LinqExtend;

namespace EF.Core.Service
{
    public abstract class ServiceListBase<TEntity, TDto> : ServiceBase<TEntity>, IServiceListBase<TEntity, TDto>
        where TEntity : EntityBase,new()
        where TDto : IQueryDto
    {

        protected abstract IQueryable<TEntity> GetQueryableByDto(TDto queryDto);

        protected virtual IQueryable<TEntity> GetQueryableByDtoFetch(IQueryable<TEntity> query)
        {
            return query;
        }

        public virtual IList<TEntity> GetListByDto(TDto queryDto)
        {
            var query = GetQueryableByDto(queryDto);

            if (query == null)
            {
                return null;
            }

            query = GetQueryableByDtoFetch(query);

            return query.ToList();
        }

        public virtual IList<TEntity> GetPagedListByDto(TDto queryDto, IDataPage dataPage, IDataOrder dataOrder)
        {
            var query = GetQueryableByDto(queryDto);

            if (query == null)
            {
                return null;
            }

            //query = query.OrderPage(dataOrder, dataPage);
            query = query.OrderUsingSortExpression(string.Format("{0} {1}", dataOrder.By, dataOrder.Order));

            query = query.Skip(dataPage.Skip).Take(dataPage.PageSize);

            query = GetQueryableByDtoFetch(query);

            return query.ToList();
        }
    }
}
