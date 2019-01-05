using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;

namespace ClinicManagement.Controllers
{
    public class FrontEducationsController : Controller
    {
        ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();
        // GET: FrontEducations
        public ActionResult Educations(int id)
        {
          
                var edu = db.Educations.Where(e => e.EducationaTypeId == id).ToList();
                ViewBag.edu = edu;
                var images = db.Images.ToList();
                ViewBag.images = images;
                return View();
        }
    }
}