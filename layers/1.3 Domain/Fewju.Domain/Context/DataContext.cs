using Fewju.Domain.CollectEntity;
using Fewju.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Fewju.Domain.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Link> Link { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Setting> Setting { get; set; }

        public DbSet<SiteSetting> SiteSetting { get; set; }
    }
}
