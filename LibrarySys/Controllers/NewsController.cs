using LibrarySys.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySys.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        LibSysEntities db = new LibSysEntities();
        public ActionResult Index(string s, int page = 1)
        {

            var index = from b in db.News select b;
            if (!string.IsNullOrEmpty(s))
            {

                index = index.Where(m => m.Article.Contains(s)
                                       || m.Subject.Contains(s)
                                       || m.Date.ToString().Contains(s));

            }

            return View(index.ToList().ToPagedList(page, 5));
        }


        [HttpGet]
        public ActionResult AddNews()
        {
            List<SelectListItem> employee = (from z in db.Employees.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = z.ID_employee + " " + z.FirstName + " " + z.LastName,
                                                 Value = z.ID_employee.ToString()
                                             }).ToList();
            ViewBag.e = employee;
            return View();
        }
        [HttpPost]
        public ActionResult AddNews(News n, HttpPostedFileBase ImagePath)
        {
            var e = db.Employees.Where(z => z.ID_employee == n.Employee.ID_employee).FirstOrDefault();
            n.Employee = e;

            db.News.Add(n);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteNews(int id)
        {
            var news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DetailNews(int ID)
        {

            var news = db.News.Find(ID);

            List<SelectListItem> screen = new List<SelectListItem>() {
        new SelectListItem { Text = "Аctive", Value = "true" },new SelectListItem {   Text = "Pasive", Value = "false" }};
            ViewBag.scr = screen;

            List<SelectListItem> employee = (from z in db.Employees.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = z.FirstName + " " + z.LastName,
                                                 Value = z.ID_employee.ToString()
                                             }).ToList();
            ViewBag.e = employee;

            return View("DetailNews", news);
        }
        public ActionResult UpdateNews(News n)
        {
            var news = db.News.Find(n.ID_news);
            news.Subject = n.Subject;
            news.Article = n.Article;
            news.Screen = n.Screen;
            var d3 = db.Employees.Where(z => z.ID_employee == n.Employee.ID_employee).FirstOrDefault();
            news.Employee = d3;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}