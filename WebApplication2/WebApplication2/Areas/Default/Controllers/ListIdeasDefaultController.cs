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
    public class ListIdeasDefaultController : Controller
    {
        private IdeaManagementEntities db = new IdeaManagementEntities();

        // GET: Admin/ListIdeas
        public ActionResult Index()
        {
            var ideas = db.Ideas.Include(i => i.Category).Include(i => i.User).Include(i => i.Topic);
            return View(ideas.ToList());
        }

        public ActionResult PopularIdea()
        {

            var ideas = db.Ideas.Include(i => i.Category).Include(i => i.User).Include(i => i.Topic);
            return View(ideas.ToList());
        }

        public ActionResult ViewIdea()
        {

            var ideas = db.Ideas.Include(i => i.Category).Include(i => i.User).Include(i => i.Topic);
            return View(ideas.ToList());
        }

        public ActionResult LatestIdea()
        {

            var ideas = db.Ideas.Include(i => i.Category).Include(i => i.User).Include(i => i.Topic);
            return View(ideas.ToList());
        }

        public ActionResult CommentIdea()
        {

            var ideas = db.Ideas.Include(i => i.Category).Include(i => i.User).Include(i => i.Topic);
            return View(ideas.ToList());
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
