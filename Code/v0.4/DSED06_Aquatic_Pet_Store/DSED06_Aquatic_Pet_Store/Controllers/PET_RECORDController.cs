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
    public class PET_RECORDController : Controller
    {
        private AQUATIC_PET_STORE_Entities db = new AQUATIC_PET_STORE_Entities();

        // GET: PET_RECORD
        public ActionResult Index()
        {
            var pET_RECORD = db.PET_RECORD.Include(p => p.PET_INFO).Include(p => p.PET_SIZE);
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
            ViewBag.PET_FK = new SelectList(db.PET_INFO, "ID_PK", "COMMON").OrderBy(x => x.Text);
            ViewBag.SIZE_FK = new SelectList(db.PET_SIZE, "ID_PK", "DESCRIPTION").
                                            OrderBy(x => x.Text, new DSED06_Aquatic_Pet_Store.Code_Common.PetSizeComparer());
            return View();
        }

        // POST: PET_RECORD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,PET_FK,SIZE_FK,CODE,DESCRIPTION")] PET_RECORD pET_RECORD)
        {
            var query = db.PET_RECORD.Select(x => x.CODE);
            if(query.Contains(pET_RECORD.CODE))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                db.PET_RECORD.Add(pET_RECORD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PET_FK = new SelectList(db.PET_INFO, "ID_PK", "COMMON", pET_RECORD.PET_FK);
            ViewBag.SIZE_FK = new SelectList(db.PET_SIZE, "ID_PK", "DESCRIPTION", pET_RECORD.SIZE_FK);
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
            ViewBag.PET_FK = new SelectList(db.PET_INFO, "ID_PK", "COMMON", pET_RECORD.PET_FK).OrderBy(x => x.Text);
            ViewBag.SIZE_FK = new SelectList(db.PET_SIZE, "ID_PK", "DESCRIPTION", pET_RECORD.SIZE_FK).
                                            OrderBy(x => x.Text, new DSED06_Aquatic_Pet_Store.Code_Common.PetSizeComparer());
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
