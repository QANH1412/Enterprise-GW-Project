using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Filter;

namespace WebApplication2.Areas.Default.Controllers
{
    [DefaultAuthorization]
    public class HomeDefaultsController : Controller
    {
        // GET: Default/HomeDefaults
        public ActionResult Index()
        {
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

         public ActionResult MusicPlayer()
        {
            ViewBag.Message = "Your MusicPlayer page.";



            return View();
        }

    }
}