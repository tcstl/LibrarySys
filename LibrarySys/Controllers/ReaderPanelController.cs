using LibrarySys.Models.Entity;
using PagedList;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LibrarySys.Controllers
{
    public class ReaderPanelController : Controller
    {
        // GET: ReaderPanel
        LibSysEntities db = new LibSysEntities();
        public ActionResult Index()
        {
            var email = (string)Session["Email"];
            var news = db.News.ToList();
            var d1 = db.Readers.Where(x => x.Email == email).Select(y => y.FirstName).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.Readers.Where(x => x.Email == email).Select(y => y.LastName).FirstOrDefault();
            ViewBag.d2 = d2;
            var d4 = db.Readers.Where(x => x.Email == email).Select(y => y.School).FirstOrDefault();
            ViewBag.d4 = d4;
            var d5 = db.Readers.Where(x => x.Email == email).Select(y => y.PhoneNumber).FirstOrDefault();
            ViewBag.d5 = d5;
            var d6 = db.Readers.Where(x => x.Email == email).Select(y => y.Email).FirstOrDefault();
            ViewBag.d6 = d6;
            var readerid = db.Readers.Where(x => x.Email == email).Select(y => y.ID_reader).FirstOrDefault();
            var d7 = db.ReaderBorrows.Where(x => x.ID_reader == readerid).Count();
            ViewBag.d7 = d7;
            var d9 = db.News.Where(x => x.Screen == true).Count();
            ViewBag.d9 = d9;
            var index = db.News.Where(x => x.Screen == true).ToList();
            return View(news);
        }
        [HttpPost]
        public ActionResult Index2(Reader r)
        {
            ScryptEncoder encoder = new ScryptEncoder();

            var user = (string)Session["Email"];
            var userItems = db.Readers.FirstOrDefault(x => x.Email == user);
            string hashedPassword = encoder.Encode("Password");
            userItems.School = r.School;
            userItems.PhoneNumber = r.PhoneNumber;
            userItems.LastName = r.LastName;
            userItems.FirstName = r.FirstName;
            userItems.Password = hashedPassword;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReadedBooks()
        {
            var reader = (string)Session["Email"];
            var ID = db.Readers.Where(x => x.Email == reader.ToString()).Select(z => z.ID_reader).FirstOrDefault();
            var index = db.ReaderBorrows.Where(x => x.ID_reader == ID).ToList();
            return View(index);
        }
        public ActionResult NewsReader()
        {
            var news = db.News.ToList();
            return View(news);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "LoginRegister");
        }
        public PartialViewResult Partial1()
        {
            var index = db.News.Where(x => x.Screen == true).ToList();
            return PartialView(index);
        }
        public PartialViewResult Partial2()
        {
            var user = (string)Session["Email"];
            var id = db.Readers.Where(x => x.Email == user).Select(y => y.ID_reader).FirstOrDefault();
            var find = db.Readers.Find(id);
            return PartialView("Partial2", find);
        }

        public ActionResult BooksSearch(string s, int page = 1)
        {
            var index = from b in db.Books select b;
            if (!string.IsNullOrEmpty(s))
            {

                index = index.Where(m => m.Title.Contains(s)
               || m.Author.FirstName.Contains(s)
               || m.Author.LastName.Contains(s)
               || m.Publisher.name.Contains(s)
               || m.Genre.name.Contains(s)
               || m.Year.Contains(s)
               || m.Pages.Contains(s));

            }
            return View(index.ToList().ToPagedList(page, 6));
        }
    }
}