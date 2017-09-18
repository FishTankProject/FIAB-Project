using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DSED06_Aquatic_Pet_Store.Models;

namespace DSED06_Aquatic_Pet_Store.Controllers
{
    public class MARINE_FAMILYController : Controller
    {
        private FIABEntities db = new FIABEntities();

        // GET: MARINE_FAMILY
        public ActionResult Index()
        {
            return View(db.MARINE_FAMILY.ToList());
        }

        // GET: MARINE_FAMILY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_FAMILY mARINE_FAMILY = db.MARINE_FAMILY.Find(id);
            if (mARINE_FAMILY == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_FAMILY);
        }

        // GET: MARINE_FAMILY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MARINE_FAMILY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,TEXT")] MARINE_FAMILY mARINE_FAMILY)
        {
            if (ModelState.IsValid)
            {
                db.MARINE_FAMILY.Add(mARINE_FAMILY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mARINE_FAMILY);
        }

        // GET: MARINE_FAMILY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_FAMILY mARINE_FAMILY = db.MARINE_FAMILY.Find(id);
            if (mARINE_FAMILY == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_FAMILY);
        }

        // POST: MARINE_FAMILY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,TEXT")] MARINE_FAMILY mARINE_FAMILY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mARINE_FAMILY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mARINE_FAMILY);
        }

        // GET: MARINE_FAMILY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_FAMILY mARINE_FAMILY = db.MARINE_FAMILY.Find(id);
            if (mARINE_FAMILY == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_FAMILY);
        }

        // POST: MARINE_FAMILY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MARINE_FAMILY mARINE_FAMILY = db.MARINE_FAMILY.Find(id);
            db.MARINE_FAMILY.Remove(mARINE_FAMILY);
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

        public ActionResult ViewList()
        {
            return View(db.MARINE_FAMILY.ToList());
        }
    }
}
