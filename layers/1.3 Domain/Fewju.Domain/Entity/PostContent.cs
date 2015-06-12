using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fewju.Domain.Entity
{
    public class PostContent
    {
        [Key]
        public long Id { get; set; }
        //public virtual Post Post { get; set; }

        public string Content { get; set; }
    }
}
