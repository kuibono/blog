using Fewju.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fewju.Domain.CollectEntity
{
    public class SiteSetting : EntityBase
    {
        [StringLength(255)]
        public string Domain { get; set; }

        public Category DefaultCategory { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Summary { get; set; }

        [StringLength(255)]
        public string Content { get; set; }
    }
}
