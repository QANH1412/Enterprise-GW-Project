﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Filter;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class CommentsController : Controller
    {
        private IdeaManagementEntities db = new IdeaManagementEntities();

        // GET: Admin/Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Idea).Include(c => c.User);
            return View(comments.ToList());
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Admin/Comments/Create
        public ActionResult Create()
        {
            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname");
            return View();
        }

        // POST: Admin/Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,Datetime,UserId,IdeaId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text", comment.IdeaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", comment.UserId);
            return View(comment);
        }

        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text", comment.IdeaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", comment.UserId);
            return View(comment);
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,Datetime,UserId,IdeaId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdeaId = new SelectList(db.Ideas, "Id", "Text", comment.IdeaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", comment.UserId);
            return View(comment);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
