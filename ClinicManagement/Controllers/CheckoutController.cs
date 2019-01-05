using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;

namespace ClinicManagement.Controllers
{
    public class CheckoutController : Controller
    {
        ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();
        // GET: Checkout

        public ActionResult Order(string amount)
        {

            ViewBag.amount = amount;
            ViewBag.Items = ShoppingCart.Items;
            return View();

        }

        public ActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PlaceOrder(string amount, string firstname, string lastname, string clinicname, string address, string city, string number)
        {

            Order order = new Order();
            order.Firstname = firstname;
            order.Lastname = lastname;
            order.Clinicname = clinicname;
            order.Address = address;
            order.City = city;
            order.Phone = number;
            order.Date = DateTime.Now;
            order.Amount = double.Parse(amount);
            db.Orders.Add(order);
            db.SaveChanges();

          

            int moid = db.Orders.Select(o => o.Id).DefaultIfEmpty(0).Max();

            var pro = from prod in ShoppingCart.Items
                      join od in db.Products
                      on prod.Id equals od.Id
                      select new {PNAME=od.SuppliedProduct.ProductName, PID = od.SuppliedProductId, PRICE = od.Price, PQTY = prod.Count };

           
            Orderdetail odt = new Orderdetail();
            foreach (var item in pro)
            {
                odt.Orderid = moid;
                odt.Productid = item.PID;
                odt.ProductName = item.PNAME;
                odt.Productprice = item.PRICE;
                odt.Productqty = item.PQTY;
                odt.Productamount = item.PQTY * item.PRICE;
                db.Orderdetails.Add(odt);
                db.SaveChanges();


                var spidt = db.SuppliedProducts.FirstOrDefault(s => s.Id == item.PID);
                spidt.AvailableQuantity -= item.PQTY;
                db.SaveChanges();

              
            }
            TempData["Message"] = "Your Order has been placed !";
            return Redirect(Request.UrlReferrer.ToString());
           
        }
    }
}