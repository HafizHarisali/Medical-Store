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
    public class SuppliedProductsController : Controller
    {
        private ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();

        // GET: SuppliedProducts
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                var suppliedProducts = db.SuppliedProducts.Include(s => s.Supplier);
                return View(suppliedProducts.ToList());
            }
            else
            {
                var searching = db.SuppliedProducts.Where(p => p.ProductName.Contains(search)).ToList();
                return View(searching);
            }


        }


        // GET: SuppliedProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuppliedProduct suppliedProduct = db.SuppliedProducts.Find(id);
            if (suppliedProduct == null)
            {
                return HttpNotFound();
            }
            return View(suppliedProduct);
        }

        // GET: SuppliedProducts/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            return View();
        }

        // POST: SuppliedProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,Price,Quantity,TotalQuantity,AvailableQuantity,TotalAmount,Datetime,SupplierId")] SuppliedProduct suppliedProduct)
        {           
            if (ModelState.IsValid)
            {

                var product = db.SuppliedProducts.FirstOrDefault(s => s.ProductName.ToLower() == suppliedProduct.ProductName.ToLower() 
                && suppliedProduct.Price == s.Price && suppliedProduct.SupplierId == s.SupplierId);

                if (product != null)
                {
                    product.Quantity = suppliedProduct.Quantity;
                    product.AvailableQuantity += suppliedProduct.Quantity;
                    product.TotalQuantity += suppliedProduct.Quantity;

                    var totalamount = product.Price * suppliedProduct.Quantity;
                    product.TotalAmount = totalamount;
                    //var grandamount = product.Price * suppliedProduct.TotalQuantity;
                    //product.GrandAmount = grandamount;
                }
                else
                {
                    var totalamount = suppliedProduct.Price * suppliedProduct.Quantity;
                    suppliedProduct.TotalAmount = totalamount;
                    //var grandamount = product.Price * suppliedProduct.TotalQuantity;
                    //product.GrandAmount = grandamount;
                    suppliedProduct.Datetime = DateTime.Now;
                    db.SuppliedProducts.Add(suppliedProduct);
                }
 
              
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", suppliedProduct.SupplierId);
            return View(suppliedProduct);
        }

        // GET: SuppliedProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuppliedProduct suppliedProduct = db.SuppliedProducts.Find(id);
            if (suppliedProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", suppliedProduct.SupplierId);
            return View(suppliedProduct);
        }

        // POST: SuppliedProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,Price,Quantity,TotalQuantity,AvailableQuantity,TotalAmount,Datetime,SupplierId")] SuppliedProduct suppliedProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suppliedProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", suppliedProduct.SupplierId);
            return View(suppliedProduct);
        }

        // GET: SuppliedProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuppliedProduct suppliedProduct = db.SuppliedProducts.Find(id);
            if (suppliedProduct == null)
            {
                return HttpNotFound();
            }
            return View(suppliedProduct);
        }

        // POST: SuppliedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SuppliedProduct suppliedProduct = db.SuppliedProducts.Find(id);
                db.SuppliedProducts.Remove(suppliedProduct);
                db.SaveChanges();
            }
            catch (Exception)
            {

                return RedirectToAction("Error","Home");
            }

            finally
            {

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

        //[HttpPost]
        //public ActionResult Index(string pid, string pqty)
        //{

        //    SuppliedProduct i = new SuppliedProduct();
        //    i.Products.FirstOrDefault().SuppliedProductId = int.Parse(pid);
        //   i.AvailableQuantity = int.Parse(pqty);
        //    db.SuppliedProducts.Add(i);
        //    db.SaveChanges();
        //    ViewBag.Items = ShoppingCart.Items;
        //    TempData["Message"] = "Your Product has been added to cart !";
        //    return RedirectToAction("Index", "SuppliedProducts");


        //}
    }
}
