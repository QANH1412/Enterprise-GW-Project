using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Filter;
using WebApplication2.Models;

namespace WebApplication2.Areas.Default.Controllers
{
    [DefaultAuthorization]
    public class UsersDefaultController : Controller
    {
        private IdeaManagementEntities db = new IdeaManagementEntities();

      

        // GET: Default/UsersDefault/Details/5
        public ActionResult Details()
        {
           
            return View();
        }

        
       

        // GET: Default/UsersDefault/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", user.DepartmentId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }

        // POST: Default/UsersDefault/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fullname,RoleId,DepartmentId,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", user.DepartmentId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }

   
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
