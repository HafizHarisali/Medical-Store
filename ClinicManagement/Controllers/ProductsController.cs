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
    public class ProductsController : Controller
    {
        private ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.MedicineType).Include(p => p.SuppliedProduct);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            var p = db.MedicineTypes.ToList();
            ViewBag.p = p;
            ViewBag.SuppliedProductId = new SelectList(db.SuppliedProducts, "Id", "ProductName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SuppliedProductId,Price,Code,Description,Medicinetypeid,CategoryId")] string medicinetypeid, Product product)
        {
            if (ModelState.IsValid)
            {
                product.Medicinetypeid = int.Parse(medicinetypeid);
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Medicinetypeid = new SelectList(db.MedicineTypes, "Id", "Name", product.Medicinetypeid);
            ViewBag.SuppliedProductId = new SelectList(db.SuppliedProducts, "Id", "ProductName", product.SuppliedProductId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            var p = db.MedicineTypes.ToList();
            ViewBag.p = p;
            ViewBag.SuppliedProductId = new SelectList(db.SuppliedProducts, "Id", "ProductName", product.SuppliedProductId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SuppliedProductId,Price,Code,Description,Medicinetypeid,CategoryId")] string medicinetypeid, Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                product.Medicinetypeid = int.Parse(medicinetypeid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
           
            ViewBag.SuppliedProductId = new SelectList(db.SuppliedProducts, "Id", "ProductName", product.SuppliedProductId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
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
            var products = db.Products.Where(p => p.Id == id).ToList();
            ViewBag.products = products;
            var imgs = db.Images.Where(i => i.Productid == id).ToList();
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
            obj.Productid = id;
            db.Images.Add(obj);
            db.SaveChanges();

            return RedirectToAction("Index", "Products");
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
            return RedirectToAction("Index", "Products");
        }
    }
}
