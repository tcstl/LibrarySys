using LibrarySys.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySys.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        LibSysEntities db = new LibSysEntities();
        public ActionResult Index(string s, int page = 1)
        {

            var index = from b in db.Authors select b;
            if (!string.IsNullOrEmpty(s))
            {

                index = index.Where(m => m.FirstName.Contains(s)
                                         || m.LastName.Contains(s));
            }
            return View(index.ToList().ToPagedList(page, 6));
        }

        // изглед ~Author/Index/
        [HttpGet]
        public ActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAuthor(Author author)
        {
            if (!ModelState.IsValid) { return View("AddAuthor"); }
            db.Authors.Add(author);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult DeleteAuthor(int ID)
        {
            var delete = db.Authors.Find(ID);
            db.Authors.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // изглед ~Author/UpdateAuthorPage/[ID_author]
        public ActionResult UpdateAuthorPage(int ID)
        {
            var update = db.Authors.Find(ID);
            return View("UpdateAuthorPage", update);
        }

        public ActionResult UpdateAuthor(Author author)
        {

            var ath = db.Authors.Find(author.ID_author);
            if (!ModelState.IsValid) { return View("UpdateAuthorPage"); }
            ath.FirstName = author.FirstName;
            ath.LastName = author.LastName;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult AuthorBooks(int ID)
        {

            var au = db.Books.Where(x => x.ID_author == ID).ToList();
            var author = db.Authors.Where(y => y.ID_author == ID).Select(z => z.FirstName + " " + z.LastName).FirstOrDefault();
            ViewBag.a = author;
            return View(au);
        }
    }
}