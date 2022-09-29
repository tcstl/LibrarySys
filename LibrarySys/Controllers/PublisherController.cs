using LibrarySys.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySys.Controllers
{
    public class PublisherController : Controller
    {
        // GET: Publisher
        LibSysEntities db = new LibSysEntities();
        public ActionResult Index(string s, int page = 1)
        {

            var index = from b in db.Publishers select b;
            if (!string.IsNullOrEmpty(s))
            {

                index = index.Where(m => m.name.Contains(s));
            }
            return View(index.ToList().ToPagedList(page, 6));
        }


        [HttpGet]
        public ActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPublisher(Publisher p)
        {
            if (!ModelState.IsValid) { return View("AddPublisher"); }
            db.Publishers.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult DeletePublisher(int ID)
        {
            var delete = db.Publishers.Find(ID);
            db.Publishers.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdatePublisherPage(int ID)
        {
            var update = db.Publishers.Find(ID);
            return View("UpdatePublisherPage", update);
        }

        public ActionResult UpdatePublisher(Publisher p)
        {
            var publ = db.Publishers.Find(p.ID_publisher);
            publ.name = p.name;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}