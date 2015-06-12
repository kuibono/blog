using Fewju.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fewju.Domain.Entity
{
    public class User:EntityBase
    {
        public User()
        {
            this.Status = UserStatus.Normal;
            this.Type = UserType.Normal;
            this.CreateDate = DateTime.Now;

        }

        public UserType Type { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Password { get; set; }
         [StringLength(255)]
        public string AvatarUrl { get; set; }
         [StringLength(255)]
         public string Description { get; set; }

         public UserStatus Status { get; set; }

         public long PostCount { get; set; }

         public long CommentCount { get;set;}

         public DateTime CreateDate { get; set; }

         public long DisplayOrder { get; set; }
    }
}
