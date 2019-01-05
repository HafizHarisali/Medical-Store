using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;
using PagedList;

namespace ClinicManagement.Controllers
{
    public class ShowOrdersController : Controller
    {
        ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();
        // GET: ShowOrders
        public ActionResult GetOrders()
        {
            var od = db.Orderdetails.OrderBy(o => o.Id).ToList();
            ViewBag.p = od;
            return View();
        }

        public ActionResult GetAllOrders()
        {
            var order = db.Orders.OrderByDescending(o => o.Id).ToList();
            ViewBag.orders = order;
            return View();
        }

        public ActionResult AllOrders()
        {
           //var order_join= from prod in ShoppingCart.Items
           //                join od in db.Products
           //                on prod.Id equals od.Id
           //                join ord in db.Orders
           //                on od.Id equals ord.Id
           //                join ordetail in db.Orderdetails
           //                on ord.Id equals ordetail.Id
           //                select new { FNAME=ord.Firstname,PCLINICNAME=ord.Clinicname,
           //                             ADRESS=ord.Address,PHONE=ord.Phone,DATE=ord.Date,
           //                             AMOUNT=ord.Amount,ORDERID=ordetail.Orderid,PNAME=ordetail.ProductName,
           //                             PQTY=ordetail.Productqty,PAMOUNT=ordetail.Productamount,
           //                             PPRICE=ordetail.Productprice
           //                };

            var olist = db.AllOrdersDetails.OrderByDescending(o =>o.Id).ToList();
            ViewBag.orderdetails = olist;
            //ViewBag.p = p;
            //var ord = db.Orders.ToList();
            //ViewBag.orders = ord;
            return View();
        }




    }
}