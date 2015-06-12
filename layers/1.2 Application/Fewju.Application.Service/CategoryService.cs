using Fewju.Domain.Context;
using Fewju.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework.Extensions;
using EntityFramework.Caching;
namespace Fewju.Application.Service
{
    public class CategoryService
    {
        DataContext context = ContextFactory.GetContext();

        public List<Category> GetAll()
        {
            return context
                .Category
                .FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(10)))
                .ToList();
        }

        public Category GetByName(string name)
        {
            return GetAll().FirstOrDefault(p => p.UrlName == name);
        }
    }
}
