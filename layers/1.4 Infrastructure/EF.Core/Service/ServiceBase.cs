using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EntityFramework.Extensions;
using Spring.Transaction.Interceptor;
using EF.Core.Context;
using EF.Core.Domain;
using EF.Core.Dto;
using EF.Core.LinqExtend;

namespace EF.Core.Service
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity>
        where TEntity : EntityBase, new()
    {
        /// <summary>
        /// 实体仓储
        /// </summary>
        public virtual DbSet<TEntity> LinqQuery
        {
            get
            {
                return CurrentContext.Set<TEntity>();
            }
        }

        public virtual DbContext CurrentContext { get; set; }

        public IList<TEntity> Query(PagerSortDto dto,Expression<Func<TEntity,bool>> exp)
        {
            return LinqQuery.Where(exp)
                .OrderUsingSortExpression(string.Format("{0} {1}",dto.sortField,dto.sortOrder))
                .Skip(dto.pageIndex*dto.pageSize)
                .Take(dto.pageSize)
                .ToList();
        }

        public virtual TEntity GetById(long id)
        {
            return id <= 0 ? null : LinqQuery.Find(id);
        }

        public virtual IList<TEntity> GetListByIds(long[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return new List<TEntity>();
            }

            IQueryable<TEntity> query;

            if (ids.Length == 1)
            {
                query = LinqQuery.Where(o => o.Id == ids[0]);
            }
            else
            {
                query = LinqQuery.Where(o => ids.Contains(o.Id));
            }

            return query.ToList();
        }

        public virtual bool IsExistById(long id)
        {
            return LinqQuery.Any(o => o.Id == id);
        }

        public virtual void Update(TEntity entiry)
        {
            CurrentContext.Entry(entiry).State = EntityState.Modified;
            CurrentContext.SaveChanges();
        }
    }
}
