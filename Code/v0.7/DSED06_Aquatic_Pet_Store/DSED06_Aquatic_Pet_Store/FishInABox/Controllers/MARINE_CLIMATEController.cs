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
    public class MARINE_CLIMATEController : Controller
    {
        private FIABEntities db = new FIABEntities();

        // GET: MARINE_CLIMATE
        public ActionResult Index(string searchString)
        {
            //gets all the data
            var status = from s in db.MARINE_CLIMATE
                         select s;

            try
            {
                //If a string is placed in the search textbox, run this
                var search = searchString;
                status = status.Where(s => s.TEXT.Contains(search));
            }
            catch
            {

            }
            return View(status);
        }

        // GET: MARINE_CLIMATE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_CLIMATE mARINE_CLIMATE = db.MARINE_CLIMATE.Find(id);
            if (mARINE_CLIMATE == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_CLIMATE);
        }

        // GET: MARINE_CLIMATE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MARINE_CLIMATE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PK,TEXT")] MARINE_CLIMATE mARINE_CLIMATE)
        {
            if (ModelState.IsValid)
            {
                db.MARINE_CLIMATE.Add(mARINE_CLIMATE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mARINE_CLIMATE);
        }

        // GET: MARINE_CLIMATE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_CLIMATE mARINE_CLIMATE = db.MARINE_CLIMATE.Find(id);
            if (mARINE_CLIMATE == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_CLIMATE);
        }

        // POST: MARINE_CLIMATE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PK,TEXT")] MARINE_CLIMATE mARINE_CLIMATE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mARINE_CLIMATE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mARINE_CLIMATE);
        }

        // GET: MARINE_CLIMATE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_CLIMATE mARINE_CLIMATE = db.MARINE_CLIMATE.Find(id);
            if (mARINE_CLIMATE == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_CLIMATE);
        }

        // POST: MARINE_CLIMATE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MARINE_CLIMATE mARINE_CLIMATE = db.MARINE_CLIMATE.Find(id);
            db.MARINE_CLIMATE.Remove(mARINE_CLIMATE);
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
