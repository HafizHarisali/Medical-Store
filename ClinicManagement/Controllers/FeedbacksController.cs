using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;
namespace ClinicManagement.Controllers
{
    public class FeedbacksController : Controller
    {
        ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();
        // GET: Feedbacks
        public ActionResult Index()
        {
            if(Session["Email"]==null)
            {
                TempData["Message"] = "Login First";
                return RedirectToAction("ClientLogin", "Front");
               
            }

       
            return View();
        }
        [HttpPost]
        public ActionResult Index(string name, string email, string subject,string comment)
        {
            Feedback feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.Subject = subject;
            feedback.Comment = comment;
            feedback.Date = DateTime.Now;
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
            return View();
        }

        public ActionResult Showcomments()
        {
            var comment = db.Feedbacks.OrderByDescending(c => c.Id).Take(15).ToList();
            ViewBag.comment = comment;
            return View();
        }
    }
}