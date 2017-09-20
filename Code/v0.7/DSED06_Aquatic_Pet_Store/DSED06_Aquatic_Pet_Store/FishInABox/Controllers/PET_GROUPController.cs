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
    public class PET_GROUPController : Controller
    {
        private AQUATIC_PET_STOREEntities db = new AQUATIC_PET_STOREEntities();

        // GET: PET_GROUP
        public ActionResult Index(string searchString)
        {
            //gets all the data
            var status = from s in db.PET_GROUP
                         select s;

            try
            {
                //If a string is placed in the search textbox, run this
                var search = searchString;
                status = status.Where(s => s.NAME.Contains(search));
            }
            catch
            {

            }

            return View(status);
        }

        // GET: PET_GROUP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_GROUP pET_GROUP = db.PET_GROUP.Find(id);
            if (pET_GROUP == null)
            {
                return HttpNotFound();
            }
            return View(pET_GROUP);
        }

        // GET: PET_GROUP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PET_GROUP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,NAME")] PET_GROUP pET_GROUP)
        {
            if (ModelState.IsValid)
            {
                db.PET_GROUP.Add(pET_GROUP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pET_GROUP);
        }

        // GET: PET_GROUP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_GROUP pET_GROUP = db.PET_GROUP.Find(id);
            if (pET_GROUP == null)
            {
                return HttpNotFound();
            }
            return View(pET_GROUP);
        }

        // POST: PET_GROUP/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,NAME")] PET_GROUP pET_GROUP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pET_GROUP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pET_GROUP);
        }

        // GET: PET_GROUP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PET_GROUP pET_GROUP = db.PET_GROUP.Find(id);
            if (pET_GROUP == null)
            {
                return HttpNotFound();
            }
            return View(pET_GROUP);
        }

        // POST: PET_GROUP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PET_GROUP pET_GROUP = db.PET_GROUP.Find(id);
            db.PET_GROUP.Remove(pET_GROUP);
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
