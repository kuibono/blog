using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fewju.Domain.Entity
{
    public class Setting:EntityBase
    {
        [StringLength(255)]
        public string SiteName { get; set; }
        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Keywords { get; set; }

        [StringLength(255)]
        public string Powerby { get; set; }

        [StringLength(255)]
        public string Copyright { get; set; }

    }
}
