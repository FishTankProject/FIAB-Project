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
    public class PET_SIZEController : Controller
    {
        private AQUATIC_PET_STOREEntities db = new AQUATIC_PET_STOREEntities();

        // GET: PET_SIZE
        public ActionResult Index(string searchString)
        {
            //gets all the data
            var status = from s in db.PET_SIZE
                         select s;

            try
            {
                //If a string is placed in the search textbox, run this
                var search = searchString;
                status = status.Where(s => s.DESCRIPTION.Contains(search));
            }
            catch
            {

            }

            return View(status);
        }

        // GET: PET_SIZE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_SIZE pET_SIZE = db.PET_SIZE.Find(id);
            if (pET_SIZE == null)
            {
                return HttpNotFound();
            }
            return View(pET_SIZE);
        }

        // GET: PET_SIZE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PET_SIZE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,DESCRIPTION")] PET_SIZE pET_SIZE)
        {
            if (ModelState.IsValid)
            {
                db.PET_SIZE.Add(pET_SIZE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pET_SIZE);
        }

        // GET: PET_SIZE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_SIZE pET_SIZE = db.PET_SIZE.Find(id);
            if (pET_SIZE == null)
            {
                return HttpNotFound();
            }
            return View(pET_SIZE);
        }

        // POST: PET_SIZE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,DESCRIPTION")] PET_SIZE pET_SIZE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pET_SIZE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pET_SIZE);
        }

        // GET: PET_SIZE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_SIZE pET_SIZE = db.PET_SIZE.Find(id);
            if (pET_SIZE == null)
            {
                return HttpNotFound();
            }
            return View(pET_SIZE);
        }

        // POST: PET_SIZE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PET_SIZE pET_SIZE = db.PET_SIZE.Find(id);
            db.PET_SIZE.Remove(pET_SIZE);
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
