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
    public class MARINE_TYPEController : Controller
    {
        private FIABEntities db = new FIABEntities();

        // GET: MARINE_TYPE
        public ActionResult Index(string searchString)
        {
            //gets all the data
            var status = from s in db.MARINE_TYPE
                         select s;

            try
            {
                //If a string is placed in the search textbox, run this
                var search = searchString;
                status = status.Where(s => s.SCHEDULE3.Contains(search));
            }
            catch
            {

            }
            return View(status);
        }

        // GET: MARINE_TYPE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_TYPE mARINE_TYPE = db.MARINE_TYPE.Find(id);
            if (mARINE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_TYPE);
        }

        // GET: MARINE_TYPE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MARINE_TYPE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,TEXT,SCHEDULE3,SCHEDULE4")] MARINE_TYPE mARINE_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.MARINE_TYPE.Add(mARINE_TYPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mARINE_TYPE);
        }

        // GET: MARINE_TYPE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_TYPE mARINE_TYPE = db.MARINE_TYPE.Find(id);
            if (mARINE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_TYPE);
        }

        // POST: MARINE_TYPE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,TEXT,SCHEDULE3,SCHEDULE4")] MARINE_TYPE mARINE_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mARINE_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mARINE_TYPE);
        }

        // GET: MARINE_TYPE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_TYPE mARINE_TYPE = db.MARINE_TYPE.Find(id);
            if (mARINE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_TYPE);
        }

        // POST: MARINE_TYPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MARINE_TYPE mARINE_TYPE = db.MARINE_TYPE.Find(id);
            db.MARINE_TYPE.Remove(mARINE_TYPE);
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
