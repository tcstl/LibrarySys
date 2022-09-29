using LibrarySys.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySys.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        LibSysEntities db = new LibSysEntities();

        // ~Book/Index
        public ActionResult Index(string s, int page = 1)
        {
            var index = from b in db.Books select b;
            // търсене по различни стойности в таблицата
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

        [HttpGet]
        //~Book/AddBook
        public ActionResult AddBook(string s)
        {

           // // category select list bring category 
            IEnumerable<SelectListItem> category = (from i in db.Genres.ToList()
                                           select new SelectListItem
                                       {
                                               Text =  i.name,
                                                 Value = i.ID_genre.ToString()
                                            }).ToList();

            ViewBag.c =category;
            
            // author select list bring author 
            List<SelectListItem> author = (from i in db.Authors.ToList()
                                         select new SelectListItem
                                        {
                                             
                                          Text =  i.FirstName + ' ' + i.LastName,
                                             Value = i.ID_author.ToString()

                                          }).ToList();

            ViewBag.a = author; //ViewBag за визуализиране на данните в изгледите

           // publisher select list bring publisher
          List<SelectListItem> publisher = (from i in db.Publishers.ToList()
                                           select new SelectListItem
                                             {
                                                Text = i.name,
                                                  Value = i.ID_publisher.ToString()
                                              }).ToList();
            ViewBag.p = publisher;
            return View();

        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {

            if (!ModelState.IsValid)
            {
                return View("AddBook");
            }
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index"); // при валидни данни връща към ~Book/Index

        }

        public ActionResult DeleteBook(int ID)
        {
            var delete = db.Books.Find(ID);
            db.Books.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //~Book/UpdateBookPage/[ID_book]
        public ActionResult UpdateBookPage(int ID)
        {
            var update = db.Books.Find(ID);

            //category select list bring category 
            List<SelectListItem> category = (from i in db.Genres.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.name,
                                                 Value = i.ID_genre.ToString()
                                             }).ToList();
            ViewBag.ct = category;

            //author select list bring author 
            List<SelectListItem> author = (from i in db.Authors.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.FirstName + ' ' + i.LastName,
                                               Value = i.ID_author.ToString()
                                           }).ToList();
            ViewBag.au = author;

            // publisher select list bring publisher
            List<SelectListItem> publisher = (from i in db.Publishers.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.name,
                                                  Value = i.ID_publisher.ToString()
                                              }).ToList();
            ViewBag.pb = publisher;

            return View("UpdateBookPage", update);
        }

        public ActionResult UpdateBook(Book book)
        {
            var bk = db.Books.Find(book.ID_book);
            bk.Title = book.Title;
            bk.Year = book.Year;
            bk.Pages = book.Pages;
            bk.ID_genre = book.ID_genre;
            bk.ID_author = book.ID_author;
            bk.ID_publisher = book.ID_publisher;
            bk.Stock = book.Stock;
           
            if (!ModelState.IsValid)
            {
                return View("UpdateBookPage");
            }

            db.SaveChanges();
            return RedirectToAction("Index"); // връща към ~Book/Index

        }
    }
}