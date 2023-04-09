using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication2.Filter;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class HomeAdminController : Controller
    {
        // GET: Admin/Home
       /* public ActionResult Index()
        {
            return View();
        }

        [HttpPost]*/
        public ActionResult Index()
        {
            /*var account = db.Users.SingleOrDefault(m => m.Id == accountId);
            Session["admin"] = account;*/
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