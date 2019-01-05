using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;

namespace ClinicManagement.Controllers
{
    public class EducationTypesController : Controller
    {
        private ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();

        // GET: EducationTypes
        public ActionResult Index()
        {
            return View(db.EducationTypes.ToList());
        }

        // GET: EducationTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationType educationType = db.EducationTypes.Find(id);
            if (educationType == null)
            {
                return HttpNotFound();
            }
            return View(educationType);
        }

        // GET: EducationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EducationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] EducationType educationType)
        {
            if (ModelState.IsValid)
            {
                db.EducationTypes.Add(educationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(educationType);
        }

        // GET: EducationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationType educationType = db.EducationTypes.Find(id);
            if (educationType == null)
            {
                return HttpNotFound();
            }
            return View(educationType);
        }

        // POST: EducationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] EducationType educationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(educationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(educationType);
        }

        // GET: EducationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationType educationType = db.EducationTypes.Find(id);
            if (educationType == null)
            {
                return HttpNotFound();
            }
            return View(educationType);
        }

        // POST: EducationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                EducationType educationType = db.EducationTypes.Find(id);
                db.EducationTypes.Remove(educationType);
                db.SaveChanges();
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
           
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
