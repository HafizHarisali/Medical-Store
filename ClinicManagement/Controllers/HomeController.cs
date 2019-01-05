using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;

namespace ClinicManagement.Controllers
{
    public class HomeController : Controller
    {
        ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();
        // GET: Home
        public ActionResult Index()
        {
            var or = db.Orders.LongCount();
            ViewBag.count_or = or;
            var com = db.Feedbacks.LongCount();
            ViewBag.count_fb = com;
            var pro = db.Products.LongCount();
            ViewBag.count_pro = pro;
            var reg = db.Registrations.LongCount();
            ViewBag.count_reg = reg;

            var total = db.Orders.ToList();
            ViewBag.total = total;

            var supplied = db.SuppliedProducts.ToList();
            ViewBag.supplied = supplied;

            var edu = db.Educations.ToList();
            ViewBag.edu = edu;

            var orderdetails = db.Orderdetails.OrderByDescending(o => o.Id).Take(15).ToList();
            ViewBag.orderdetail = orderdetails;
            var orders = db.Orders.OrderByDescending(o => o.Id).Take(15).ToList();
            ViewBag.order = orders;
          

            if (Session["Email"]==null)
            {
                TempData["Error"] = "Login First";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult GetProducts(int id)
        {
            var products = db.Products.Where(m => m.CategoryId == id).ToList();
            ViewBag.pro = products;
            
            return View();
        }

       
    }
}