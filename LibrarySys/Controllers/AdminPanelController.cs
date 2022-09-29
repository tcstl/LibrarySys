using LibrarySys.Models;
using LibrarySys.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LibrarySys.Controllers
{
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        LibSysEntities db = new LibSysEntities();
        public ActionResult Index()
        {
            var email = (string)Session["Email"];



            var news = db.News.ToList();
            var d1 = db.Employees.Where(x => x.Email == email).Select(y => y.FirstName).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.Employees.Where(x => x.Email == email).Select(y => y.LastName).FirstOrDefault();
            ViewBag.d2 = d2;

            var d5 = db.Employees.Where(x => x.Email == email).Select(y => y.Adress).FirstOrDefault();
            ViewBag.d5 = d5;
            var d6 = db.Employees.Where(x => x.Email == email).Select(y => y.Email).FirstOrDefault();
            ViewBag.d6 = d6;

            var d7 = db.Books.Count();
            ViewBag.d7 = d7;
            var d8 = db.Readers.Count();
            ViewBag.d8 = d8;
            var d9 = db.ReaderBorrows.Where(x => x.Process == true).Count();
            ViewBag.d9 = d9;
            var d12 = db.ReaderBorrows.Where(x => x.Process == false).Count();
            ViewBag.d12 = d12;
            var d10 = db.News.Count();
            ViewBag.d10 = d10;
            return View(news);
        }
        [HttpPost]
        public ActionResult Index2(Employee e)
        {
            var user = (string)Session["Email"];
            var employee = db.Employees.FirstOrDefault(x => x.Email == user);
            employee.Password = e.Password;
            employee.FirstName = e.FirstName;
            employee.LastName = e.LastName;
            employee.Adress = e.Adress;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NewsEmployee()
        {
            var news = db.News.ToList();
            return View(news);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminLogin");
        }
        public PartialViewResult Partial1()
        {
            var index = db.News.Where(x => x.Screen == true).ToList();
            return PartialView(index);
        }
        public PartialViewResult Partial2()
        {
            var user = (string)Session["Email"];
            var id = db.Employees.Where(x => x.Email == user).Select(y => y.ID_employee).FirstOrDefault();
            var find = db.Employees.Find(id);
            return PartialView("Partial2", find);
        }
        public ActionResult Statistics()
        {
            var d2 = db.Readers.Count();
            ViewBag.d2 = d2;

            var d1 = db.Books.Count();
            ViewBag.d1 = d1;

            var d4 = db.ReaderBorrows.Where(x => x.Process == true).Count();
            ViewBag.d4 = d4;

            var d5 = db.ReaderBorrows.Where(x => x.Process == false).Count();
            ViewBag.d5 = d5;

            ClassTop10 cs = new ClassTop10();
            cs.topauthor = db.Top10Author().ToList();
            cs.topreader = db.Top10Reader().ToList();
            cs.topbook = db.Top10ReadedBook().ToList();

            return View(cs);
        }

      

        
    }
}