using Fewju.Application.IService;
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
    public class SettingService : ISettingService
    {
        DataContext context = ContextFactory.GetContext();
        public Setting Get()
        {
            var set = context.Setting.FromCacheFirstOrDefault(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(10)));
            if (set == null)
            {
                set = new Setting();
                context.Setting.Add(set);
                context.SaveChanges();
            }
            return set;
        }
    }
}
