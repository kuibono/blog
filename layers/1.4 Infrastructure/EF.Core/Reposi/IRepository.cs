using EF.Core.Domain;
using EF.Core.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EF.Core.Reposi
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        DbSet<TEntity> LinqQuery { get; }

        DbRawSqlQuery<TDto> SQLUniqueResult<TDto>(string hqlString,
            IDictionary<string, object> parameterValues = null);

        IList<TDto> SQLQuery<TDto>(string hqlString);

        IList<TDto> HQLQuery<TDto>(string hqlString, IDataPage dataPage,
            IDictionary<string, object> parameterValues = null);

        TEntity Get(object id);

        long Save(TEntity entity);

        void Update(TEntity entity);

        void SaveOrUpdate(TEntity entity);

        void Delete(TEntity entity);

        int Delete(Expression<Func<TEntity, bool>> exp);

        IQueryable Query(Expression<Func<TEntity, bool>> exp);

        IQueryable Query(Expression<Func<TEntity, bool>> exp, IDataPage pager);

    }
}
