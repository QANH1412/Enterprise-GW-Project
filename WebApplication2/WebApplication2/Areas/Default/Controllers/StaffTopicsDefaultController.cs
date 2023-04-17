using PagedList;
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
    public class StaffTopicsDefaultController : Controller
    {
        private IdeaManagementEntities db = new IdeaManagementEntities();


        // GET: StaffTopics
        public ActionResult Index(int? page, int? pageSize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 5;
            }

            var staffTopics = db.Topics.ToList();
            return View(staffTopics.ToPagedList((int)page, (int)pageSize));
            //  return View(db.Topics.ToList());
        }




        // GET: Default/StaffTopicsDefault/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Default/StaffTopicsDefault/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/StaffTopicsDefault/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ClosureDate,FinalClosureDate")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        // GET: Default/StaffTopicsDefault/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Default/StaffTopicsDefault/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ClosureDate,FinalClosureDate")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Default/StaffTopicsDefault/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Default/StaffTopicsDefault/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
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
