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
    public class MARINE_CLASSController : Controller
    {
        private FIABEntities db = new FIABEntities();

        // GET: MARINE_CLASS
        public ActionResult Index(string searchString)
        {
            //gets all the data
            var status = from s in db.MARINE_CLASS
                         select s;

            try
            {
                //If a string is placed in the search textbox, run this
                var search = searchString;
                status = status.Where(s => s.SCHEDULE4.Contains(search));
            }
            catch
            {

            }
            return View(status);
        }

        // GET: MARINE_CLASS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_CLASS mARINE_CLASS = db.MARINE_CLASS.Find(id);
            if (mARINE_CLASS == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_CLASS);
        }

        // GET: MARINE_CLASS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MARINE_CLASS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,TEXT,SCHEDULE4")] MARINE_CLASS mARINE_CLASS)
        {
            if (ModelState.IsValid)
            {
                db.MARINE_CLASS.Add(mARINE_CLASS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mARINE_CLASS);
        }

        // GET: MARINE_CLASS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_CLASS mARINE_CLASS = db.MARINE_CLASS.Find(id);
            if (mARINE_CLASS == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_CLASS);
        }

        // POST: MARINE_CLASS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,TEXT,SCHEDULE4")] MARINE_CLASS mARINE_CLASS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mARINE_CLASS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mARINE_CLASS);
        }

        // GET: MARINE_CLASS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_CLASS mARINE_CLASS = db.MARINE_CLASS.Find(id);
            if (mARINE_CLASS == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_CLASS);
        }

        // POST: MARINE_CLASS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MARINE_CLASS mARINE_CLASS = db.MARINE_CLASS.Find(id);
            db.MARINE_CLASS.Remove(mARINE_CLASS);
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
