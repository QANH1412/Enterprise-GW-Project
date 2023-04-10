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
    public class ReactsController : Controller
    {
        private IdeaManagementEntities db = new IdeaManagementEntities();

        // GET: Admin/Reacts
        public ActionResult Index()
        {
            var reacts = db.Reacts.Include(r => r.Idea).Include(r => r.User);
            return View(reacts.ToList());
        }

        // GET: Admin/Reacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            React react = db.Reacts.Find(id);
            if (react == null)
            {
                return HttpNotFound();
            }
            return View(react);
        }

        // GET: Admin/Reacts/Create
        public ActionResult Create()
        {
            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname");
            return View();
        }

        // POST: Admin/Reacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,React1,UserId,IdeaId")] React react)
        {
            if (ModelState.IsValid)
            {
                db.Reacts.Add(react);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text", react.IdeaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", react.UserId);
            return View(react);
        }

        // GET: Admin/Reacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            React react = db.Reacts.Find(id);
            if (react == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text", react.IdeaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", react.UserId);
            return View(react);
        }

        // POST: Admin/Reacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,React1,UserId,IdeaId")] React react)
        {
            if (ModelState.IsValid)
            {
                db.Entry(react).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text", react.IdeaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", react.UserId);
            return View(react);
        }

        // GET: Admin/Reacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            React react = db.Reacts.Find(id);
            if (react == null)
            {
                return HttpNotFound();
            }
            return View(react);
        }

        // POST: Admin/Reacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            React react = db.Reacts.Find(id);
            db.Reacts.Remove(react);
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
