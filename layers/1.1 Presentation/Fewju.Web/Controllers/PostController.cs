using Fewju.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Fewju.Web.Controllers
{
    public class PostController : Controller
    {
        PostService PostService = new PostService();
        CategoryService CategoryService = new CategoryService();

        public ActionResult Index(long id)
        {
            var result = PostService.GetById(id);
            return RedirectToAction("ByName", new { name = result.UrlName });
        }

        public ActionResult ByName(string name)
        {
            var result = PostService.GetByUrlName(name);
            return View("Index", result);
        }

        public ActionResult PostList(int? page)
        {
            if(page==null)
            {
                page = 1;
            }
            var result = PostService.QueryPostList(page.Value, 20);
            return PartialView(result);
        }

        public ActionResult PostTop5()
        {
            var result = PostService.GetTopPosts(5);
            return View(result);
        }
        public ActionResult PostTop5Views()
        {
            ViewBag.ModelName = "最热文章";
            var result = PostService.GetTopViewPosts(5);
            return View("PostTop5", result);
        }

        public ActionResult CategoryList()
        {
            var result = CategoryService.GetAll();
            return View(result);
        }

        public ActionResult Menu()
        {
            var result = CategoryService.GetAll();
            return View(result);
        }

        public ActionResult ByUserName(string name)
        {
            return View("PostList", PostService.QueryByUserName(name));
        }

        public ActionResult Query(string s)
        {
            return View("PostList", PostService.QueryByKey(s));
        }
    }
}
