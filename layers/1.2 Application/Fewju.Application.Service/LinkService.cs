using EntityFramework.Caching;
using Fewju.Domain.Context;
using Fewju.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework.Extensions;

namespace Fewju.Application.Service
{
    public class LinkService
    {
        DataContext context = ContextFactory.GetContext();
        public List<Link> GetAll()
        {
            return context
                .Link
                .FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(10)))
                .OrderBy(p => p.Displayorder)
                .ThenBy(p=>p.Id)
                .ToList();
        }
    }
}
