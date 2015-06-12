using Fewju.Application.IService;
using Fewju.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fewju.Web.Controllers.Basement
{
    public class MvcBaseController : Controller
    {
        //
        // GET: /MvcBase/

        ISettingService SettingService = new SettingService();

        //public override 
        public ActionResult Index()
        {
            return View();
        }

    }
}
