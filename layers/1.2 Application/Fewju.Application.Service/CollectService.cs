using Fewju.Domain.CollectEntity;
using Fewju.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fewju.Application.Service
{
    public class CollectService
    {
        DataContext context = ContextFactory.GetContext();

        public List<SiteSetting> GetAll()
        {
            return context.SiteSetting.ToList();
        }

        public void Create(SiteSetting set)
        {
            context.SiteSetting.Add(set);
            context.SaveChanges();
        }
    }
}
