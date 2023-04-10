using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication2.Filter;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class IdeasController : Controller
    {
        private IdeaManagementEntities db = new IdeaManagementEntities();
        int viewCount;
        
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


        // function add view
      /*  public ActionResult Create([Bind(Include = "Id,VisitTime,UserId,IdeaId")] View view)
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
        }*/



        // GET: Ideas/Details/5
        public ActionResult Details(int? id, int TopicId, string Name, DateTime ClosureDate, DateTime FinalClosureDate, int userId)
        {
            
            // Viewbag to redirect to cshtml and go back to index page
            ViewBag.Topic = new Topic
            {
                Id = TopicId,
                Name = Name,
                ClosureDate = ClosureDate,
                FinalClosureDate = FinalClosureDate

            };

          
            //View
            viewCount = (from item in db.Views 
                         where item.IdeaId == id 
                         where item.UserId == userId 
                         select item).Count();
            viewCount += 1;
            ViewBag.ViewCount = viewCount;  

            // ADD VIEW to database
            View view = new View
            {
                UserId = userId,
                IdeaId = id,
                VisitTime = DateTime.Now
            };
            db.Views.Add(view);
            db.SaveChanges();

             // React : like and dislike
           /*  var reactCount = (from item in db.Reacts 
                               where item.IdeaId == id 
                               where item.UserId == userId
                               select item).SingleOrDefault();*/
            var reactCount = db.Reacts.SingleOrDefault(m => m.IdeaId == id && m.UserId == userId);
            


            if (reactCount == null) {
                ViewBag.React = new React
                {
                    UserId = userId,
                    IdeaId = id,
                    React1 = 0

                };

            }
            else
            {
                ViewBag.React = new React
                {
                    UserId = userId,
                    IdeaId = id,
                    React1 = reactCount.React1

                };
               
            }

            //generate code
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

        [HttpPost]
        public ActionResult LikeButton(int IdeaId, int TopicId, string Name, DateTime ClosureDate, DateTime FinalClosureDate, int UserId, int reactReact)
        {
            reactReact += 1;
            var react = db.Reacts.SingleOrDefault(m => m.IdeaId == IdeaId && m.UserId == UserId);

            if(react == null)
            {
                React reactt = new React
                {
                    UserId = UserId,
                    IdeaId = IdeaId,
                    React1 = reactReact
                };
                db.Reacts.Add(reactt);
                db.SaveChanges();
            }
            else
            {
                react.React1 = reactReact;
                db.SaveChanges();
            }


            // return RedirectToAction("Index",new{ topicId = TopicId, name = Name, closureDate = ClosureDate, finalClosureDate = FinalClosureDate });
            return RedirectToRoute(new { controller = "Ideas", action = "Index", id = TopicId, name = Name, closuredate = ClosureDate, finalclosuredate = FinalClosureDate });

        }

        [HttpPost]
        public ActionResult DisLikeButton(int IdeaId, int TopicId, string Name, DateTime ClosureDate, DateTime FinalClosureDate, int UserId, int reactReact)
        {
            reactReact -= 1;
            var react = db.Reacts.SingleOrDefault(m => m.IdeaId == IdeaId && m.UserId == UserId);

            if (react == null)
            {
                React reactt = new React
                {
                    UserId = UserId,
                    IdeaId = IdeaId,
                    React1 = reactReact
                };
                db.Reacts.Add(reactt);
                db.SaveChanges();
            }
            else
            {
                react.React1 = reactReact;
                db.SaveChanges();
            }


            // return RedirectToAction("Index",new{ topicId = TopicId, name = Name, closureDate = ClosureDate, finalClosureDate = FinalClosureDate });
            return RedirectToRoute(new { controller = "Ideas", action = "Index", id = TopicId, name = Name, closuredate = ClosureDate, finalclosuredate = FinalClosureDate });

        }




        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LikeButton(int? id, int TopicId, string Name, DateTime ClosureDate, DateTime FinalClosureDate, int userId)
        {
            var reactCount = db.Reacts.SingleOrDefault(m => m.IdeaId == id && m.UserId == userId);

            reactCount.React1 += 1;

            ViewBag.reactReact = reactCount.React1;

            React react = new React
            {
                React1 = reactCount.React1,
                UserId = userId,
                IdeaId = id,
            };
            db.Reacts.Add(react);
            db.SaveChanges();

            return RedirectToRoute(new { controller = "Ideas", action = "Details", Id = id, name = Name, closuredate = ClosureDate, finalclosuredate = FinalClosureDate });
        }*/






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
        public ActionResult Create([Bind(Include = "Id,Text,FilePath,Datetime,UserId,CategoryId,TopicId")] Idea idea, int Id, string Name, DateTime ClosureDate, DateTime FinalClosureDate, int userId)
        {
            if (ModelState.IsValid)
            {
                // gán giá trị topic id và user id
                idea.TopicId = Id;
                idea.UserId = userId;


                db.Ideas.Add(idea);
                db.SaveChanges();
                return RedirectToRoute(new { controller = "Ideas", action = "Index", id = Id, name = Name, closuredate = ClosureDate, finalclosuredate = FinalClosureDate });
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
