using Fewju.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fewju.Domain.Entity
{
    public class Post : EntityBase
    {
        public Post()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.Status = PostStatus.Normal;
            this.Categories = new List<Category>();
            this.Content = new PostContent();
        }

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string UrlName { get; set; }

        public User User { get; set; }

        public ICollection<Category> Categories { get; set; }

        [StringLength(512)]
        public string Summary { get; set; }

        public bool EnableComment { get; set; }

        public long CommentCount { get; set; }

        public long ViewCount { get; set; }

        public long Recommend { get; set; }

        public PostStatus Status { get; set; }

        public bool SetTop { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [Required]
        public virtual PostContent Content { get; set; }
    }
}
