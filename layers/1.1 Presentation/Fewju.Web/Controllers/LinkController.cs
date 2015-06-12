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
    public class LinkController : Controller
    {
        LinkService LinkService = new LinkService();
        public ActionResult LinkList()
        {
            return View(LinkService.GetAll());
        }
    }
}
