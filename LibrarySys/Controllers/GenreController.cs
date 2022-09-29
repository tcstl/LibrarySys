using LibrarySys.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySys.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
        LibSysEntities db = new LibSysEntities();
        public ActionResult Index(string s, int page = 1)
        {

            var index = from b in db.Genres select b;
            if (!string.IsNullOrEmpty(s))
            {

                index = index.Where(m => m.name.Contains(s));
            }
            return View(index.ToList().ToPagedList(page, 6));
        }
        [HttpGet]
        public ActionResult ADDCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ADDCategory(Genre c)
        {
            if (!ModelState.IsValid) { return View("AddCategory"); }
            db.Genres.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int ID)
        {
            var delete = db.Genres.Find(ID);
            db.Genres.Remove(delete);
            // status.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateCategoryPage(int ID)
        {
            var update = db.Genres.Find(ID);
            return View("UpdateCategoryPage", update);
        }

        public ActionResult UpdateCategory(Genre c)
        {
            var ctg = db.Genres.Find(c.ID_genre);
            ctg.name = c.name;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}