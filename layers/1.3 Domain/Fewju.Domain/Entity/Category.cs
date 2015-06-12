using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fewju.Domain.Entity
{
    public class Category:EntityBase
    {
        public string Name { get; set; }

        public string UrlName { get; set; }

        public string Summary { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
