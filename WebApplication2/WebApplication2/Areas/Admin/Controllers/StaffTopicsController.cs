using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using WebApplication2.Filter;
using WebApplication2.Models;


namespace WebApplication2.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class StaffTopicsController : Controller
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

       // [HttpPost]
        public void ExportToExcel(int id)
        {
           var ideaList = (from idea in db.Ideas
                          where idea.TopicId == id
                          select idea).ToList();

            var ideaCount = (from idea in db.Ideas
                             where idea.TopicId == id
                             select idea).Count();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "No.";
            ws.Cells["B1"].Value = "Text";
            ws.Cells["C1"].Value = "FilePath";
            ws.Cells["D1"].Value = "View";
            ws.Cells["E1"].Value = "Like";
            ws.Cells["F1"].Value = "DisLike";


            foreach (var idea in ideaList)
            {
            

                // viewcount
                var viewCount = (from Item in db.Views
                                 where Item.IdeaId == idea.Id
                                 select Item).Count();

                ws.Cells[String.Format("A{0}", ideaCount)].Value = idea.Id;
                ws.Cells[String.Format("B{0}", ideaCount)].Value = idea.Text;
                ws.Cells[String.Format("C{0}", ideaCount)].Value = idea.FilePath;
                ws.Cells[String.Format("D{0}", ideaCount)].Value = viewCount;


                var reactCount = (from Item in db.Reacts
                                  where Item.IdeaId == idea.Id
                                  select Item.React1).Sum();
                if (reactCount == null)
                {
                    reactCount = 0;
                }

                if (reactCount == 0)
                {
                    var reactLike = 0;
                    ws.Cells[String.Format("E{0}", ideaCount)].Value = reactLike;
                    var reactDisLike = 0;
                    ws.Cells[String.Format("F{0}", ideaCount)].Value = reactDisLike;
                }

                

                // like 
                if (reactCount < 0)
                    {
                        var reactLike = 0;
                        ws.Cells[String.Format("E{0}", ideaCount)].Value = reactLike;

                    }
                    else
                    {
                        var reactLike = reactCount;
                        ws.Cells[String.Format("E{0}", ideaCount)].Value = reactLike;

                    }
                    // dislike
                    if (reactCount > 0)
                    {
                        var reactDisLike = 0;
                        ws.Cells[String.Format("F{0}", ideaCount)].Value = reactDisLike;

                    }
                    else
                    {
                        var reactDislike = reactCount;
                        ws.Cells[String.Format("F{0}", ideaCount)].Value = reactDislike;
                    }

                
               
                ideaCount++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

          //  return View("Index");


            //    ws.Cells["A{ExcelId}"].Value = ExcelId;


            /* foreach(var idea in ideaList)
             {
                 idea.
             }*/


            /* List<Idea> ideaList = db.Ideas.Select(x => x.TopicId == TopicId);
             {

             }).ToList();*/
        }











        // GET: StaffTopics/Details/5
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

        // GET: StaffTopics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffTopics/Create
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

        // GET: StaffTopics/Edit/5
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

        // POST: StaffTopics/Edit/5
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

        // GET: StaffTopics/Delete/5
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

        // POST: StaffTopics/Delete/5
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
