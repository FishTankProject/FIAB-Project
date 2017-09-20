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
    public class MPI_SRFIROController : Controller
    {
        private FIABEntities db = new FIABEntities();

        // GET: MPI_SRFIRO
        public ActionResult Index(string searchString)
        {
            //gets all the data
            var status = from s in db.MPI_SRFIRO
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

        // GET: MPI_SRFIRO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPI_SRFIRO mPI_SRFIRO = db.MPI_SRFIRO.Find(id);
            if (mPI_SRFIRO == null)
            {
                return HttpNotFound();
            }
            return View(mPI_SRFIRO);
        }

        // GET: MPI_SRFIRO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MPI_SRFIRO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,TEXT_ID,TEXT")] MPI_SRFIRO mPI_SRFIRO)
        {
            if (ModelState.IsValid)
            {
                db.MPI_SRFIRO.Add(mPI_SRFIRO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mPI_SRFIRO);
        }

        // GET: MPI_SRFIRO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPI_SRFIRO mPI_SRFIRO = db.MPI_SRFIRO.Find(id);
            if (mPI_SRFIRO == null)
            {
                return HttpNotFound();
            }
            return View(mPI_SRFIRO);
        }

        // POST: MPI_SRFIRO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,TEXT_ID,TEXT")] MPI_SRFIRO mPI_SRFIRO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mPI_SRFIRO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mPI_SRFIRO);
        }

        // GET: MPI_SRFIRO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPI_SRFIRO mPI_SRFIRO = db.MPI_SRFIRO.Find(id);
            if (mPI_SRFIRO == null)
            {
                return HttpNotFound();
            }
            return View(mPI_SRFIRO);
        }

        // POST: MPI_SRFIRO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MPI_SRFIRO mPI_SRFIRO = db.MPI_SRFIRO.Find(id);
            db.MPI_SRFIRO.Remove(mPI_SRFIRO);
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
