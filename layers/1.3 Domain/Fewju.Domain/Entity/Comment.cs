using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fewju.Domain.Entity
{
    public class Comment : EntityBase
    {
        public Comment Parent { get; set; }

        public Post Post { get; set; }

        public User User { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string SiteUrl { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        [StringLength(50)]
        public string IpAddress { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Approved { get; set; }
    }
}
