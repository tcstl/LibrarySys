using LibrarySys.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySys.Controllers
{
    public class BorrowController : Controller
    {
        // GET: Borrow
        LibSysEntities db = new LibSysEntities();
        public ActionResult Index(string s, int page = 1)
        {
            var index = from b in db.ReaderBorrows.Where(x => x.Process == false) select b;
            if (!string.IsNullOrEmpty(s))
            {
                index = index.Where(m => m.Book.Title.Contains(s)||
                 m.Employee.LastName.Contains(s)||
                 m.Employee.FirstName.Contains(s)||
                 m.Reader.LastName.Contains(s)||
                 m.Reader.FirstName.Contains(s)||
                  m.Book.Author.LastName.Contains(s)||
                  m.Book.Author.FirstName.Contains(s)||
                  m.CheckoutDate.ToString().Contains(s) ||
                  m.ReturnDate.ToString().Contains(s)
                );

            }
            return View(index.ToList().ToPagedList(page, 6));
        }
        public ActionResult Archive(string s, int page = 1)
        {
            var index = from b in db.ReaderBorrows.Where(b => b.Process == true) select b;
            if (!string.IsNullOrEmpty(s))
            {
                index = index.Where(m => m.Book.Title.Contains(s)
                 ||  m.ID_employee.ToString().Contains(s)
                 ||  m.ID_reader.ToString().Contains(s)
                 ||  m.Reader.LastName.Contains(s)
                 ||  m.Reader.FirstName.Contains(s)
                 ||  m.Employee.LastName.Contains(s)
                 ||  m.Employee.FirstName.ToString().Contains(s)
                 ||  m.CheckoutDate.ToString().Contains(s)
                  || m.ReturnReader.ToString().Contains(s)
                );
            }
            return View(index.ToList().ToPagedList(page, 6));

        }

        [HttpGet]
        public ActionResult Borrow()
        {
            List<SelectListItem> reader = (from x in db.Readers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.FirstName + " " + x.LastName,
                                               Value = x.ID_reader.ToString()
                                           }).ToList();
            List<SelectListItem> book = (from y in db.Books.Where(x => x.Available == true).ToList()
                                         select new SelectListItem
                                         {
                                             Text = y.Title + " " + y.Author.FirstName + " " + y.Author.LastName,
                                             Value = y.ID_book.ToString()
                                         }).ToList();

            List<SelectListItem> employee = (from z in db.Employees.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = z.FirstName + " " + z.LastName,
                                                 Value = z.ID_employee.ToString()
                                             }).ToList();
            ViewBag.r = reader;
            ViewBag.b = book;
            ViewBag.e = employee;
            return View();
        }
        [HttpPost]
        public ActionResult Borrow(ReaderBorrow p)
        {

            var d1 = db.Readers.Where(x => x.ID_reader == p.Reader.ID_reader).FirstOrDefault();
            var d2 = db.Books.Where(y => y.ID_book == p.Book.ID_book).FirstOrDefault();
            var d3 = db.Employees.Where(z => z.ID_employee == p.Employee.ID_employee).FirstOrDefault();
            p.Reader = d1;
            p.Book = d2;
            p.Employee = d3;
            db.ReaderBorrows.Add(p);
            db.SaveChanges();
           
            return RedirectToAction("Index");
        }

        public ActionResult ReaderReturn(int ID)
        {
            var odn = db.ReaderBorrows.Find(ID);
            DateTime d1 = DateTime.Parse(odn.ReturnDate.ToString());         
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;            //разликата между датата на която трябва да се върне книгата и която е била върната
            ViewBag.dgr = d3.TotalDays; 
            return View("ReaderReturn", odn);
        }
        public ActionResult Update(ReaderBorrow p)
        {
            var hrk = db.ReaderBorrows.Find(p.ID_borrow);
            hrk.ID_book = p.ID_book;
            hrk.ID_reader = p.ID_reader;
            hrk.ID_employee = p.ID_employee;
            hrk.ReturnReader = p.ReturnReader;
            hrk.ReturnReader = p.ReturnReader;
            hrk.Process = p.Process;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}