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
    public class MARINE_SPECIESController : Controller
    {
        private FIABEntities db = new FIABEntities();

        // GET: MARINE_SPECIES
        public ActionResult Index(string searchString)
        {
            var mARINE_SPECIES = db.MARINE_SPECIES.Include(m => m.MARINE_CLASS).Include(m => m.MARINE_FAMILY);

            try
            {
                //If a string is placed in the search textbox, run this
                var search = searchString;
                mARINE_SPECIES = mARINE_SPECIES.Where(s => s.SCIENTIFIC.Contains(search));
            }
            catch
            {

            }

            return View(mARINE_SPECIES.ToList());
        }

        // GET: MARINE_SPECIES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_SPECIES mARINE_SPECIES = db.MARINE_SPECIES.Find(id);
            if (mARINE_SPECIES == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_SPECIES);
        }

        // GET: MARINE_SPECIES/Create
        public ActionResult Create()
        {
            ViewBag.CLASS_FK = new SelectList(db.MARINE_CLASS, "ID_PK", "TEXT");
            ViewBag.FAMILY_FK = new SelectList(db.MARINE_FAMILY, "ID_PK", "TEXT");
            return View();
        }

        // POST: MARINE_SPECIES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,CLASS_FK,SPECIES_FK,SCIENTIFIC,COMMON,TEXT,FAMILY_FK")] MARINE_SPECIES mARINE_SPECIES)
        {
            if (ModelState.IsValid)
            {
                db.MARINE_SPECIES.Add(mARINE_SPECIES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CLASS_FK = new SelectList(db.MARINE_CLASS, "ID_PK", "TEXT", mARINE_SPECIES.CLASS_FK);
            ViewBag.FAMILY_FK = new SelectList(db.MARINE_FAMILY, "ID_PK", "TEXT", mARINE_SPECIES.FAMILY_FK);
            return View(mARINE_SPECIES);
        }

        // GET: MARINE_SPECIES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_SPECIES mARINE_SPECIES = db.MARINE_SPECIES.Find(id);
            if (mARINE_SPECIES == null)
            {
                return HttpNotFound();
            }
            ViewBag.CLASS_FK = new SelectList(db.MARINE_CLASS, "ID_PK", "TEXT", mARINE_SPECIES.CLASS_FK);
            ViewBag.FAMILY_FK = new SelectList(db.MARINE_FAMILY, "ID_PK", "TEXT", mARINE_SPECIES.FAMILY_FK);
            return View(mARINE_SPECIES);
        }

        // POST: MARINE_SPECIES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,CLASS_FK,SPECIES_FK,SCIENTIFIC,COMMON,TEXT,FAMILY_FK")] MARINE_SPECIES mARINE_SPECIES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mARINE_SPECIES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CLASS_FK = new SelectList(db.MARINE_CLASS, "ID_PK", "TEXT", mARINE_SPECIES.CLASS_FK);
            ViewBag.FAMILY_FK = new SelectList(db.MARINE_FAMILY, "ID_PK", "TEXT", mARINE_SPECIES.FAMILY_FK);
            return View(mARINE_SPECIES);
        }

        // GET: MARINE_SPECIES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_SPECIES mARINE_SPECIES = db.MARINE_SPECIES.Find(id);
            if (mARINE_SPECIES == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_SPECIES);
        }

        // POST: MARINE_SPECIES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MARINE_SPECIES mARINE_SPECIES = db.MARINE_SPECIES.Find(id);
            db.MARINE_SPECIES.Remove(mARINE_SPECIES);
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
