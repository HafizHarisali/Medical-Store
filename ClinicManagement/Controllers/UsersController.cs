using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Models;

namespace ClinicManagement.Controllers
{
    public class UsersController : Controller
    {
        ClinicManagmentSystemEntities db = new ClinicManagmentSystemEntities();
        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            ViewBag.users = users;
            return View();
        }
    }
}