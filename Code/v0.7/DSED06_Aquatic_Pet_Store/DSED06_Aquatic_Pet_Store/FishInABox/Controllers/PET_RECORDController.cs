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
    public class PET_RECORDController : Controller
    {
        private AQUATIC_PET_STOREEntities db = new AQUATIC_PET_STOREEntities();

        // GET: PET_RECORD
        public ActionResult Index(string searchString)
        {
            var pET_RECORD = db.PET_RECORD.Include(p => p.PET_INFO).Include(p => p.PET_SIZE).Include(p => p.RECORD_PACKING);

            try
            {
                //If a string is placed in the search textbox, run this
                var search = searchString;
                pET_RECORD = pET_RECORD.Where(s => s.DESCRIPTION.Contains(search));
            }
            catch
            {

            }

            return View(pET_RECORD.ToList());
        }

        // GET: PET_RECORD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_RECORD pET_RECORD = db.PET_RECORD.Find(id);
            if (pET_RECORD == null)
            {
                return HttpNotFound();
            }
            return View(pET_RECORD);
        }

        // GET: PET_RECORD/Create
        public ActionResult Create()
        {
            ViewBag.PET_FK = new SelectList(db.PET_INFO, "ID_PK", "COMMON");
            ViewBag.SIZE_FK = new SelectList(db.PET_SIZE, "ID_PK", "DESCRIPTION");
            ViewBag.ID_PK = new SelectList(db.RECORD_PACKING, "RECORD_FK", "RECORD_FK");
            return View();
        }

        // POST: PET_RECORD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,PET_FK,SIZE_FK,CODE,DESCRIPTION")] PET_RECORD pET_RECORD)
        {
            if (ModelState.IsValid)
            {
                db.PET_RECORD.Add(pET_RECORD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PET_FK = new SelectList(db.PET_INFO, "ID_PK", "COMMON", pET_RECORD.PET_FK);
            ViewBag.SIZE_FK = new SelectList(db.PET_SIZE, "ID_PK", "DESCRIPTION", pET_RECORD.SIZE_FK);
            ViewBag.ID_PK = new SelectList(db.RECORD_PACKING, "RECORD_FK", "RECORD_FK", pET_RECORD.ID_PK);
            return View(pET_RECORD);
        }

        // GET: PET_RECORD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_RECORD pET_RECORD = db.PET_RECORD.Find(id);
            if (pET_RECORD == null)
            {
                return HttpNotFound();
            }
            ViewBag.PET_FK = new SelectList(db.PET_INFO, "ID_PK", "COMMON", pET_RECORD.PET_FK);
            ViewBag.SIZE_FK = new SelectList(db.PET_SIZE, "ID_PK", "DESCRIPTION", pET_RECORD.SIZE_FK);
            ViewBag.ID_PK = new SelectList(db.RECORD_PACKING, "RECORD_FK", "RECORD_FK", pET_RECORD.ID_PK);
            return View(pET_RECORD);
        }

        // POST: PET_RECORD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,PET_FK,SIZE_FK,CODE,DESCRIPTION")] PET_RECORD pET_RECORD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pET_RECORD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PET_FK = new SelectList(db.PET_INFO, "ID_PK", "COMMON", pET_RECORD.PET_FK);
            ViewBag.SIZE_FK = new SelectList(db.PET_SIZE, "ID_PK", "DESCRIPTION", pET_RECORD.SIZE_FK);
            ViewBag.ID_PK = new SelectList(db.RECORD_PACKING, "RECORD_FK", "RECORD_FK", pET_RECORD.ID_PK);
            return View(pET_RECORD);
        }

        // GET: PET_RECORD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_RECORD pET_RECORD = db.PET_RECORD.Find(id);
            if (pET_RECORD == null)
            {
                return HttpNotFound();
            }
            return View(pET_RECORD);
        }

        // POST: PET_RECORD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PET_RECORD pET_RECORD = db.PET_RECORD.Find(id);
            db.PET_RECORD.Remove(pET_RECORD);
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
