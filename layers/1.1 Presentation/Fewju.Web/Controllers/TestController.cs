using Fewju.Application.Service;
using Fewju.Domain.Context;
using Fewju.Domain.Entity;
using Fewju.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fewju.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        DataContext context = ContextFactory.GetContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePost()
        {
            Setting set = new Setting();
            set.Copyright = "&copy; Fewju 2015";
            set.Description = "";
            set.Keywords = "C#,ASP.NET";
            set.Powerby = "Fewju.com";
            set.SiteName = "Fewju";
            context.Setting.Add(set);
            context.SaveChanges();


            Category cat = new Category();
            cat.Name = "未分类";
            cat.Summary = "";
            cat.UrlName = "UnCategory";
            context.Category.Add(cat);
            context.SaveChanges();

            User u = new Domain.Entity.User();
            u.AvatarUrl = "";
            u.CommentCount = 0;
            u.CreateDate = DateTime.Now;
            u.Description = "";
            u.DisplayOrder = 0;
            u.Name = "admin";
            u.Password = "Admin@123";
            u.PostCount = 0;
            u.Status = UserStatus.Normal;
            u.Type = UserType.Normal;
            u.UserName = "admin";
            context.User.Add(u);
            context.SaveChanges();

            Post p = new Post();
            p.Categories = new List<Category>();
            p.Categories.Add(context.Category.FirstOrDefault());
            p.CommentCount = 0;
            p.Content = new PostContent { Content = "test content" };
            p.EnableComment = true;
            p.Recommend=0;
            p.SetTop = true;
            p.Status = PostStatus.Normal;
            p.Summary = "test content";
            p.Title = "test title";
            p.UrlName = "test";
            p.User = context.User.FirstOrDefault();
            p.ViewCount = 0;

            context.Post.Add(p);
            context.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
