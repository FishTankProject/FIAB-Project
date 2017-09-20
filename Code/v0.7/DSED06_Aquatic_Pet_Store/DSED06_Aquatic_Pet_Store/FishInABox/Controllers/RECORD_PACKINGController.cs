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
    public class RECORD_PACKINGController : Controller
    {
        private AQUATIC_PET_STOREEntities db = new AQUATIC_PET_STOREEntities();

        // GET: RECORD_PACKING
        public ActionResult Index(string searchString)
        {
            var rECORD_PACKING = db.RECORD_PACKING.Include(r => r.PET_RECORD);

            try
            {
                //If a string is placed in the search textbox, run this
                var search = Convert.ToDouble(searchString);
                rECORD_PACKING = rECORD_PACKING.Where(s => s.BAG == (search));
            }
            catch
            {

            }

            return View(rECORD_PACKING.ToList());
        }

        // GET: RECORD_PACKING/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECORD_PACKING rECORD_PACKING = db.RECORD_PACKING.Find(id);
            if (rECORD_PACKING == null)
            {
                return HttpNotFound();
            }
            return View(rECORD_PACKING);
        }

        // GET: RECORD_PACKING/Create
        public ActionResult Create()
        {
            ViewBag.RECORD_FK = new SelectList(db.PET_RECORD, "ID_PK", "CODE");
            return View();
        }

        // POST: RECORD_PACKING/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RECORD_FK,BAG,BOX")] RECORD_PACKING rECORD_PACKING)
        {
            if (ModelState.IsValid)
            {
                db.RECORD_PACKING.Add(rECORD_PACKING);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RECORD_FK = new SelectList(db.PET_RECORD, "ID_PK", "CODE", rECORD_PACKING.RECORD_FK);
            return View(rECORD_PACKING);
        }

        // GET: RECORD_PACKING/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECORD_PACKING rECORD_PACKING = db.RECORD_PACKING.Find(id);
            if (rECORD_PACKING == null)
            {
                return HttpNotFound();
            }
            ViewBag.RECORD_FK = new SelectList(db.PET_RECORD, "ID_PK", "CODE", rECORD_PACKING.RECORD_FK);
            return View(rECORD_PACKING);
        }

        // POST: RECORD_PACKING/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RECORD_FK,BAG,BOX")] RECORD_PACKING rECORD_PACKING)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rECORD_PACKING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RECORD_FK = new SelectList(db.PET_RECORD, "ID_PK", "CODE", rECORD_PACKING.RECORD_FK);
            return View(rECORD_PACKING);
        }

        // GET: RECORD_PACKING/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECORD_PACKING rECORD_PACKING = db.RECORD_PACKING.Find(id);
            if (rECORD_PACKING == null)
            {
                return HttpNotFound();
            }
            return View(rECORD_PACKING);
        }

        // POST: RECORD_PACKING/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RECORD_PACKING rECORD_PACKING = db.RECORD_PACKING.Find(id);
            db.RECORD_PACKING.Remove(rECORD_PACKING);
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
