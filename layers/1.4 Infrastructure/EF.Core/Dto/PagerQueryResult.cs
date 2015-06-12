using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Dto
{
    public class PagerQueryResult<TEntity> where TEntity: class,new()
    {
        public PagerQueryResult()
        {

        }
        public PagerQueryResult(List<TEntity> _data, long _total)
        {
            this.data = _data;
            this.total = _total;
        }
        public List<TEntity> data { get; set; }

        public long total { get; set; }
    }
}
