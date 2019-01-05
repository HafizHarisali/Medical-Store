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
    public class EducationsController : Controller
    {
        private ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();

        // GET: Educations
        public ActionResult Index()
        {
            var educations = db.Educations.Include(e => e.EducationType).Include(e => e.Organizer).Include(e => e.Teacher);
            return View(educations.ToList());
        }

        // GET: Educations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // GET: Educations/Create
        public ActionResult Create()
        {
            ViewBag.EducationaTypeId = new SelectList(db.EducationTypes, "Id", "Name");
            ViewBag.OrganizerId = new SelectList(db.Organizers, "Id", "Name");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name");
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Topic,Subject,Fees,Date,Duration,TeacherId,OrganizerId,EducationaTypeId,Time")] Education education)
        {
            if (ModelState.IsValid)
            {
                db.Educations.Add(education);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EducationaTypeId = new SelectList(db.EducationTypes, "Id", "Name", education.EducationaTypeId);
            ViewBag.OrganizerId = new SelectList(db.Organizers, "Id", "Name", education.OrganizerId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", education.TeacherId);
            return View(education);
        }

        // GET: Educations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            ViewBag.EducationaTypeId = new SelectList(db.EducationTypes, "Id", "Name", education.EducationaTypeId);
            ViewBag.OrganizerId = new SelectList(db.Organizers, "Id", "Name", education.OrganizerId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", education.TeacherId);
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Topic,Subject,Fees,Date,Duration,TeacherId,OrganizerId,EducationaTypeId,Time")] Education education)
        {
            if (ModelState.IsValid)
            {
                db.Entry(education).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EducationaTypeId = new SelectList(db.EducationTypes, "Id", "Name", education.EducationaTypeId);
            ViewBag.OrganizerId = new SelectList(db.Organizers, "Id", "Name", education.OrganizerId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", education.TeacherId);
            return View(education);
        }

        // GET: Educations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Education education = db.Educations.Find(id);
                db.Educations.Remove(education);
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

        public ActionResult Images(int id)
        {
            var education = db.Educations.Where(e => e.Id == id).ToList();
            ViewBag.education = education;
            var imgs = db.Images.Where(i => i.Educationid == id).ToList();
            ViewBag.imgs = imgs;
            return View();


        }

        [HttpPost]
        public ActionResult Images(int id, HttpPostedFileBase file)
        {
            string path = System.IO.Path.Combine("~/Content/Images/" + file.FileName);
            file.SaveAs(Server.MapPath(path));
            Image obj = new Image();
            obj.Imagename = file.FileName.ToString();
            obj.Educationid = id;
            db.Images.Add(obj);
            db.SaveChanges();

            return RedirectToAction("Index", "Educations");
        }



        public ActionResult DeleteImage(int? id)
        {
            Image img = db.Images.Find(id);
            return View(img);
        }



        [HttpPost]
        public ActionResult DeleteImage(int id)
        {
            Image img = db.Images.Find(id);
            db.Images.Remove(img);
            db.SaveChanges();
            return RedirectToAction("Index", "Educations");
        }
    }
}
