using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FishInABox.Models;

namespace FishInABox.Controllers
{
    public class MPI_SRFIRO_DETAILController : Controller
    {
        private FIABEntities db = new FIABEntities();

        // GET: MPI_SRFIRO_DETAIL
        public ActionResult Index(string searchString)
        {
            //gets all the data
            var status = from s in db.MPI_SRFIRO_DETAIL
                         select s;

            try
            {
                //If a string is placed in the search textbox, run this
                var search = searchString;
                status = status.Where(s => s.TEXT.Contains(search));
            }
            catch
            {

            }
            return View(status);
        }

        // GET: MPI_SRFIRO_DETAIL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPI_SRFIRO_DETAIL mPI_SRFIRO_DETAIL = db.MPI_SRFIRO_DETAIL.Find(id);
            if (mPI_SRFIRO_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(mPI_SRFIRO_DETAIL);
        }

        // GET: MPI_SRFIRO_DETAIL/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MPI_SRFIRO_DETAIL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,SRFIRO_FK,TEXT_ID,TEXT")] MPI_SRFIRO_DETAIL mPI_SRFIRO_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.MPI_SRFIRO_DETAIL.Add(mPI_SRFIRO_DETAIL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mPI_SRFIRO_DETAIL);
        }

        // GET: MPI_SRFIRO_DETAIL/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPI_SRFIRO_DETAIL mPI_SRFIRO_DETAIL = db.MPI_SRFIRO_DETAIL.Find(id);
            if (mPI_SRFIRO_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(mPI_SRFIRO_DETAIL);
        }

        // POST: MPI_SRFIRO_DETAIL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,SRFIRO_FK,TEXT_ID,TEXT")] MPI_SRFIRO_DETAIL mPI_SRFIRO_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mPI_SRFIRO_DETAIL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mPI_SRFIRO_DETAIL);
        }

        // GET: MPI_SRFIRO_DETAIL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPI_SRFIRO_DETAIL mPI_SRFIRO_DETAIL = db.MPI_SRFIRO_DETAIL.Find(id);
            if (mPI_SRFIRO_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(mPI_SRFIRO_DETAIL);
        }

        // POST: MPI_SRFIRO_DETAIL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MPI_SRFIRO_DETAIL mPI_SRFIRO_DETAIL = db.MPI_SRFIRO_DETAIL.Find(id);
            db.MPI_SRFIRO_DETAIL.Remove(mPI_SRFIRO_DETAIL);
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
