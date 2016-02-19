using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OptionsWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(String.Compare((String) TempData["Success"], "true", true) == 0)
            {
                ViewBag.Message = "Your changes were saved!";
                TempData["Success"] = null;
            }
            else if(String.Compare((String)TempData["Success"], "false", true) == 0)
            {
                ViewBag.Message = "Error! Your changes WERE NOT saved!";
                TempData["Success"] = null;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}