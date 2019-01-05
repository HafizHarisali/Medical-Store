using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;
using ClinicManagement;

namespace ClinicManagement.Controllers
{
    public class FrontController : Controller
    {
        ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();
        // GET: Front
        public ActionResult Index()
        {
            var product = db.Products.OrderByDescending(p => p.Id).Take(15).ToList();
            ViewBag.product = product;
            var education = db.Educations.OrderByDescending(e => e.Id).Take(2).ToList();
            ViewBag.education = education;
            var images = db.Images.ToList();
            ViewBag.images = images;
            return View();
        }

        public ActionResult ClientSignup(int id = 2)
        {
            var user = db.Usertypes.Where(ut => ut.Id == id).ToList();
            ViewBag.user = user;
            return View();
        }

        [HttpPost]
        public ActionResult ClientSignup(int id, string name, byte age, string contact, string address, string email, string password)
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
            return RedirectToAction("ClientLogin", "Front");
        }


        public ActionResult ClientLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientLogin(string email, string password)
        {

            Usertype ut = new Usertype();
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.Usertype.Name == "Client");
            if (user != null)
            {
                Session["Id"] = user.Id;
                Session["Name"] = user.Name;
                Session["Age"] = user.Age;
                Session["Contact"] = user.Contact;
                Session["Address"] = user.Address;
                Session["Email"] = user.Email;
                return RedirectToAction("Index", "Front");

            }

            else
            {
               
                TempData["Message"] = "Invalid Username Or Password";
            }
            return View();
        }

        public ActionResult AllProducts()
        {
            var cat = db.Categories.ToList();
            ViewBag.cat = cat;
            var allpro = db.Products.ToList();
            ViewBag.allpro = allpro;
            var images = db.Images.ToList();
            ViewBag.images = images;
            return View();
        }
     
        public ActionResult Product(int id)
        {
            var cat = db.Categories.ToList();
            ViewBag.cat = cat;
            var medicine = db.Products.Where(m => m.CategoryId == id).ToList();
            ViewBag.medicine = medicine;
            var images = db.Images.ToList();
            ViewBag.images = images;
            var cid = medicine.FirstOrDefault().CategoryId;
            var cname = db.Categories.Where(c => c.Id == cid);
            ViewBag.cname = cname;
          
            return View();
        }

        

        public ActionResult ProductDetails(int id)
        {
          
            var prodetail = db.Products.Where(p => p.Id == id).ToList();
            ViewBag.pro = prodetail;
            var images = db.Images.Where(i => i.Productid == id).ToList();
            ViewBag.images = images;
            var cid = prodetail.FirstOrDefault().CategoryId;
            var cname = db.Categories.Where(c => c.Id == cid);
            ViewBag.cname = cname;
            return View();
        }

      
       public ActionResult Cart()
        {
           
            ViewBag.Items = ShoppingCart.Items;
            return View();
        }

        [HttpPost]
        public ActionResult Cart(string pid, string pqty)
        {
            //var pro = db.Products.ToList();
            //foreach (var item in pro)
            //{
            //    if (int.Parse(pqty) > item.SuppliedProduct.AvailableQuantity)
            //    {
                    
            //        TempData["Error"]= "Out Of Stock";
            //    }
            //}

            foreach (var item in ShoppingCart.Items)
                {


                    if (item.Id == int.Parse(pid))
                    {
                        item.Count += int.Parse(pqty);
                        ViewBag.Items = ShoppingCart.Items;
                        TempData["Message"] = "Your Cart has been updated !";
                        return Redirect(Request.UrlReferrer.ToString());

                    }


                }
                ProductItems i = new ProductItems() { Id = int.Parse(pid), Count = int.Parse(pqty) };
                ShoppingCart.Items.Add(i);
                ViewBag.Items = ShoppingCart.Items;
                TempData["Message"] = "Your Product has been added to cart !";
                return Redirect(Request.UrlReferrer.ToString());
        }

     
        //[HttpPost]
        //public ActionResult RemoveCart(string pid, string pqty)
        //{
        //    ProductItems i = new ProductItems();
        //    foreach (var item in ShoppingCart.Items)
        //    {
        //        if(item.Id==int.Parse(pid))
        //        {
        //            ShoppingCart.Items.Remove(i);
        //        }
        //    }
           
        //    return RedirectToAction("Cart","Front");
        //}

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email,string subject, string comment)
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

        public ActionResult About()
        {
            return View();
        }
    }
}