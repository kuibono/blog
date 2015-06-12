using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fewju.Domain.Entity
{
    public class Link:EntityBase
    {
        public Link()
        {
            this.Target = "_blank";
        }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Href { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long Displayorder { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Display { get; set; }
    }
}
