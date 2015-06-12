using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace EF.Core.Context
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            var dbContext = CallContext.GetData("DataContext") as DbContext;
            if (dbContext == null)  //线程在数据槽里面没有此上下文
            {
                //CallContext.GetData("DataContext") as DbContext;
                DbContext context=new DbContext();

                CallContext.SetData("DataContext", new );
                return context;
            }
            return dbContext;
        }
    }
}
