using Fewju.Domain.Context;
using Fewju.Domain.Entity;
using Fewju.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EntityFramework.Extensions;

namespace Fewju.Application.Service
{
    public class PostService
    {
        DataContext context = ContextFactory.GetContext();

        CategoryService cs = new CategoryService();
        public Post GetById(long id)
        {

            var result = context
                .Post
                .Include("User")
                .Include("Categories")
                .Include("Content").FirstOrDefault(p => p.Id == id);
            context.Post.Where(p => p.Id == id).Update(x => new Post { ViewCount = x.ViewCount + 1 });
            return result;
        }

        public Post GetByUrlName(string urlName)
        {
            context.Post.Where(p => p.UrlName == urlName).Update(x => new Post { ViewCount = x.ViewCount + 1 });
            return context
                .Post
                .Include("User")
                .Include("Categories")
                .Include("Content")
                .FirstOrDefault(p => p.UrlName == urlName);
        }
        public IQueryable<Post> Query(Expression<Func<Post, bool>> exp, int page = 1, int pageSize = 10)
        {
            return context
                .Post
                .Include("User")
                .Include("Categories")
                .Where(exp)
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
        public List<Post> QueryByUserId(long id, int page = 1, int pageSize = 10)
        {
            return Query(p => p.User.Id == id, page, pageSize).ToList();
        }
        public List<Post> QueryByUserName(string name, int page = 1, int pageSize = 10)
        {
            return Query(p => p.User.UserName == name, page, pageSize).ToList();
        }
        public List<Post> QueryByCategoryName(string CategoryName, int page = 1, int pageSize = 10)
        {
            return Query(p => p.Categories.Any(x => x.UrlName == CategoryName), page, pageSize).ToList();
        }
        public List<Post> QueryByKey(string key, int page = 1, int pageSize = 10)
        {
            return Query(
                p => p.UrlName.Contains(key)
                    || p.Summary.Contains(key)
                    || p.Title.Contains(key)
                , page, pageSize).ToList();
        }

        public List<Post> QueryPostList(int page = 1, int pageSize = 10)
        {
            return Query(
                p => p.Status==PostStatus.Normal, page, pageSize).ToList();
        }
        public List<Post> GetTopPosts(int top)
        {
            return context
                .Post
                .Include("User")
                .Include("Categories")
                .Where(p => p.Status == PostStatus.Normal)
                .OrderByDescending(p => p.CreateDate)
                .Take(top)
                .ToList();
        }
        public List<Post> GetTopViewPosts(int top)
        {
            return context
                .Post
                .Include("User")
                .Include("Categories")
                .Where(p => p.Status == PostStatus.Normal)
                .OrderByDescending(p => p.ViewCount)
                .Take(top)
                .ToList();
        }
        public long Create(Post post, string Domain)
        {

            string urlName = post.UrlName;

            if (post.User == null)
            {
                post.User = context.User.First();
            }
            var cats = context.Category.ToList();
            foreach (var cat in cats)
            {
                var keys = cat.Summary.Split(',').ToList();
                keys.Add(cat.Name);
                keys.Add(cat.UrlName);
                if (keys.Any(p => post.Content.Content.ToLower().Contains(p) || post.Title.ToLower().Contains(p)))
                {
                    if (post.Categories.Contains(cat) == false)
                    {
                        post.Categories.Add(cat);
                    }
                }
            }

            if (post.Categories.Count == 0)
            {
                var setSetting = context.SiteSetting.Include("DefaultCategory").FirstOrDefault(p => p.Domain == Domain);
                if (setSetting != null)
                {
                    post.Categories.Add(setSetting.DefaultCategory);
                }
            }

            var q = from l in context.Post where l.UrlName == post.UrlName orderby l.UrlName select l;
            if (q.Count() > 0)
            {
                post.UrlName += "_1";
                if (q.Count() > 0)
                {
                    int maxNumber = Convert.ToInt32(q.Last().UrlName.Split('_').Last());
                    post.UrlName = urlName + "_" + maxNumber;
                }
            }
            context.Post.Add(post);
            context.SaveChanges();
            return post.Id;
        }
    }
}
