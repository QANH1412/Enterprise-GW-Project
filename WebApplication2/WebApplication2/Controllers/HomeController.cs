﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private IdeaManagementEntities db = new IdeaManagementEntities();

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

        public ActionResult Login()
        {
            ViewBag.Message = "Your Login page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            // check db
            IdeaManagementEntities db = new IdeaManagementEntities();
            var account = db.Users.SingleOrDefault(m => m.Email.ToLower() == email.ToLower() && m.Password == password   );

            // check code
            if (account != null)
            {
                // tạo session và gán giá trị
                Session["user"] = account;
                string role = account.RoleId;


                if (role == "Admin")
                {
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index");

                }


            }
            else
            {
                TempData["error"] = "Not correct";
                return View();
            }


            /*var user = db.Users.Where(x => x.Email == log.Email && x.Password == log.Password).Count();
            if (user > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }*/

        }
        
       
    }
}