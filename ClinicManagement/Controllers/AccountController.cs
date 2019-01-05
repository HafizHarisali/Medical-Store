using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;

namespace ClinicManagement.Controllers
{
    public class AccountController : Controller
    {
        ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();
        // GET: Account
        public ActionResult Signup(int id=1)
        {
            var user = db.Usertypes.Where(ut => ut.Id == id).ToList();
            ViewBag.user = user;
            return View();
        }

        [HttpPost]
        public ActionResult Signup(int id,string name, byte age, string contact, string address, string email, string password)
        {
            
            User u = new User();
            u.UsertypeId = id;
            u.Name = name;
            u.Age = age;
            u.Contact = contact;
            u.Address = address;
            u.Email = email;
            u.Password = password;
            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("Login","Account");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            Usertype ut = new Usertype();
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.Usertype.Name == "Admin");
           if(user!=null)
            {
                Session["Id"] = user.Id;
                Session["Email"] = user.Email;
                Session["Name"] = user.Name;
                return RedirectToAction("Index", "Home");
                
            }

            else
            {
                TempData["Message"] = "Invalid Username Or Password";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}