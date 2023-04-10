using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    public class ViewsController : Controller
    {
        private IdeaManagementEntities db = new IdeaManagementEntities();

        // GET: Admin/Views
        public ActionResult Index()
        {
            var views = db.Views.Include(v => v.Idea).Include(v => v.User);
            return View(views.ToList());
        }

        // GET: Admin/Views/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View view = db.Views.Find(id);
            if (view == null)
            {
                return HttpNotFound();
            }
            return View(view);
        }

        // GET: Admin/Views/Create
        public ActionResult Create()
        {
            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname");
            return View();
        }

        // POST: Admin/Views/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VisitTime,UserId,IdeaId")] View view)
        {
            if (ModelState.IsValid)
            {
                db.Views.Add(view);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text", view.IdeaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", view.UserId);
            return View(view);
        }

        // GET: Admin/Views/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View view = db.Views.Find(id);
            if (view == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text", view.IdeaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", view.UserId);
            return View(view);
        }

        // POST: Admin/Views/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VisitTime,UserId,IdeaId")] View view)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text", view.IdeaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", view.UserId);
            return View(view);
        }

        // GET: Admin/Views/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View view = db.Views.Find(id);
            if (view == null)
            {
                return HttpNotFound();
            }
            return View(view);
        }

        // POST: Admin/Views/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            View view = db.Views.Find(id);
            db.Views.Remove(view);
            db.SaveChanges();
            return RedirectToAction("Index");
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
