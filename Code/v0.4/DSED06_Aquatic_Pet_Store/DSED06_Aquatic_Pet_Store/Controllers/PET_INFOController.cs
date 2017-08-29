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
    public class PET_INFOController : Controller
    {
        private AQUATIC_PET_STORE_Entities db = new AQUATIC_PET_STORE_Entities();

        // GET: PET_INFO
        public ActionResult Index()
        {
            var pET_INFO = db.PET_INFO.Include(p => p.PET_GROUP);
            return View(pET_INFO.ToList());
        }

        // GET: PET_INFO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_INFO pET_INFO = db.PET_INFO.Find(id);
            if (pET_INFO == null)
            {
                return HttpNotFound();
            }
            return View(pET_INFO);
        }

        // GET: PET_INFO/Create
        public ActionResult Create()
        {
            ViewBag.GROUP_FK = new SelectList(db.PET_GROUP, "ID_PK", "NAME").OrderBy(x => x.Text);
            return View();
        }

        // POST: PET_INFO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,GROUP_FK,COMMON,SCIENTIFIC")] PET_INFO pET_INFO)
        {
            if (ModelState.IsValid)
            {
                db.PET_INFO.Add(pET_INFO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GROUP_FK = new SelectList(db.PET_GROUP, "ID_PK", "NAME", pET_INFO.GROUP_FK);
            return View(pET_INFO);
        }

        // GET: PET_INFO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_INFO pET_INFO = db.PET_INFO.Find(id);
            if (pET_INFO == null)
            {
                return HttpNotFound();
            }
            ViewBag.GROUP_FK = new SelectList(db.PET_GROUP, "ID_PK", "NAME", pET_INFO.GROUP_FK).OrderBy(x => x.Text);
            return View(pET_INFO);
        }

        // POST: PET_INFO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,GROUP_FK,COMMON,SCIENTIFIC")] PET_INFO pET_INFO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pET_INFO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GROUP_FK = new SelectList(db.PET_GROUP, "ID_PK", "NAME", pET_INFO.GROUP_FK);
            return View(pET_INFO);
        }

        // GET: PET_INFO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_INFO pET_INFO = db.PET_INFO.Find(id);
            if (pET_INFO == null)
            {
                return HttpNotFound();
            }
            return View(pET_INFO);
        }

        // POST: PET_INFO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PET_INFO pET_INFO = db.PET_INFO.Find(id);
            db.PET_INFO.Remove(pET_INFO);
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
