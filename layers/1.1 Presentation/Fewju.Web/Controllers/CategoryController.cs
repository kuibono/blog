using Fewju.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fewju.Web.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        PostService PostService = new PostService();
        CategoryService CategoryService = new CategoryService();

        public ActionResult Index(string name)
        {
            var result = PostService.QueryByCategoryName(name);
            var category = CategoryService.GetByName(name);
            if(category!=null)
            {
                ViewBag.Title = category.Name;
                ViewBag.Description = category.Summary;
                ViewBag.Keywords = category.Name + "," + category.UrlName;
            }
            return View(result);
        }

    }
}
