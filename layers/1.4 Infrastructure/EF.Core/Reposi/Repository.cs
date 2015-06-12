using EF.Core.Context;
using EF.Core.Domain;
using EF.Core.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using EntityFramework.Extensions;
using System.Linq.Expressions;

namespace EF.Core.Reposi
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        public virtual DbSet<TEntity> LinqQuery
        {
            get
            {
                return DbContextFactory.GetCurrentDbContext().Set<TEntity>();
            }
        }

        public virtual DbContext currentContext
        {
            get
            {
                return DbContextFactory.GetCurrentDbContext();
            }
        }

        public virtual DbRawSqlQuery<TDto> SQLUniqueResult<TDto>(string hqlString,
            IDictionary<string, object> parameterValues = null)
        {
            return DbContextFactory.GetCurrentDbContext().Database.SqlQuery<TDto>(hqlString, parameterValues);
        }

        public virtual IList<TDto> SQLQuery<TDto>(string hqlString)
        {
            return DbContextFactory.GetCurrentDbContext().Database.SqlQuery<TDto>(hqlString).ToList();
        }


        public virtual IList<TDto> HQLQuery<TDto>(string hqlString, IDataPage dataPage,
            IDictionary<string, object> parameterValues = null)
        {
            return this.SQLUniqueResult<TDto>(hqlString, parameterValues)
                .Skip(dataPage.Skip)
                .Take(dataPage.PageSize)
                .ToList();

        }


        public virtual TEntity Get(object id)
        {
            return LinqQuery.Find(id);
        }

        public virtual long Save(TEntity entity)
        {

            LinqQuery.Add(entity);
            currentContext.SaveChanges();
            return entity.Id;
        }

        public virtual void Update(TEntity entity)
        {
            LinqQuery.Where(p => p.Id == entity.Id).Update(x => entity);
        }

        public virtual void SaveOrUpdate(TEntity entity)
        {
            LinqQuery.Attach(entity);
            currentContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            LinqQuery.Delete(p => p.Id == entity.Id);
        }

        public virtual int Delete(Expression<Func<TEntity,bool>> exp)
        {
            return LinqQuery.Delete(exp);
        }

        public IQueryable Query(Expression<Func<TEntity,bool>> exp)
        {
            return LinqQuery.Where(exp);
        }

        public IQueryable Query(Expression<Func<TEntity, bool>> exp,IDataPage pager)
        {
            return LinqQuery.Where(exp).Skip(pager.Skip).Take(pager.PageSize);
        }

    }
}
