using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fewju.Domain.Entity
{
    public class Tag:EntityBase
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string UrlName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long Displayorder { get; set; }

        public DateTime CreateDate { get; set; }

        public long Count { get; set; }
    }
}
