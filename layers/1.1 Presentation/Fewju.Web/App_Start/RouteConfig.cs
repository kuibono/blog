using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fewju.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Post",
                url: "p/{name}",
                defaults: new { controller = "Post", action = "ByName", name = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Category",
                url: "c/{name}",
                defaults: new { controller = "Category", action = "Index", name = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "User",
                url: "u/{name}",
                defaults: new { controller = "Post", action = "ByUserName", name = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "Query",
            //    url: "p/{s}",
            //    defaults: new { controller = "Post", action = "Query", s = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}