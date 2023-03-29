using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class IdeasController : Controller
    {
        private IdeaManagementEntities db = new IdeaManagementEntities();

        // GET: Ideas
        public ActionResult Index(int id, string Name, DateTime ClosureDate, DateTime FinalClosureDate)
        {
            // Viewbag these info to push to INDEX 
            ViewBag.Topic = new Topic
            {
                Id = id,
                Name = Name,
                ClosureDate = ClosureDate,
                FinalClosureDate = FinalClosureDate

            };
            var Result = (from item in db.Ideas where item.TopicId == id select item).ToList(); 
            return View(Result);
        }

        // GET: Ideas/Details/5
        public ActionResult Details(int? id, int TopicId, string Name, DateTime ClosureDate, DateTime FinalClosureDate)
        {
            // Viewbag to redirect to cshtml and go back to index page
            ViewBag.Topic = new Topic
            {
                Id = TopicId,
                Name = Name,
                ClosureDate = ClosureDate,
                FinalClosureDate = FinalClosureDate

            };


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // GET: Ideas/Create
        public ActionResult Create(int Id, string Name, DateTime ClosureDate, DateTime FinalClosureDate)
        {
            // Viewbag to redirect to cshtml and go back to index page
            ViewBag.Topic = new Topic
            {
                Id = Id,
                Name = Name,
                ClosureDate = ClosureDate,
                FinalClosureDate = FinalClosureDate

            };

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname");
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name");
            return View();
        }

        // POST: Ideas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,FilePath,Datetime,UserId,CategoryId,TopicId")] Idea idea, int Id, string Name, DateTime ClosureDate, DateTime FinalClosureDate)
        {
            if (ModelState.IsValid)
            {
                idea.TopicId = Id;
                db.Ideas.Add(idea);
                db.SaveChanges();
                return RedirectToRoute(new {controller = "Ideas", action = "Index" , id = Id, name = Name, closuredate = ClosureDate, finalclosuredate = FinalClosureDate });
            }

           

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", idea.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", idea.UserId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", idea.TopicId);

            

            return View(idea);


        }

        // GET: Ideas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", idea.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", idea.UserId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", idea.TopicId);
            return View(idea);
        }

        // POST: Ideas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,FilePath,Datetime,UserId,CategoryId,TopicId")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(idea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", idea.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", idea.UserId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", idea.TopicId);
            return View(idea);
        }

        // GET: Ideas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // POST: Ideas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Idea idea = db.Ideas.Find(id);
            db.Ideas.Remove(idea);
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
