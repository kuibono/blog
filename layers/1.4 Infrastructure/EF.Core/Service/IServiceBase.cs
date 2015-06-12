using EF.Core.Domain;
using EF.Core.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EF.Core.Service
{
    public interface IServiceBase<TEntity> 
        where TEntity : EntityBase,new()
    {

        //DbSet<TEntity> LinqQuery { get; }

        //DbContext CurrentContext { get; set; }

        IList<TEntity> Query(PagerSortDto dto, Expression<Func<TEntity, bool>> exp);

        TEntity GetById(long id);

        IList<TEntity> GetListByIds(long[] ids);

        bool IsExistById(long id);

        void Update(TEntity entiry);
    }
}
