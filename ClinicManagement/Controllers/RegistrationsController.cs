using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;

namespace ClinicManacgement.Controllers
{
    public class RegistrationsController : Controller
    {
        ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();
        // GET: Registrations
        public ActionResult Index(int id)
        {
            
            var reg = db.Educations.Where(e => e.Id == id).ToList();
            ViewBag.reg = reg;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int id,string name, string email, int age, string contact)
        {
            Registration reg = new Registration();
            reg.EducationId = id;
            reg.Name = name;
            reg.Email = email;
            reg.Age = age;
            reg.Contact = contact;
            db.Registrations.Add(reg);
            db.SaveChanges();
            TempData["Message"] = "You registered successfully !";
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult RegisteredStudents()
        {
            var RS = db.Registrations.ToList();
            ViewBag.regstd = RS;
            return View();
        }
    }
}