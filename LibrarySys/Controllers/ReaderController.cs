using LibrarySys.Models.Entity;
using PagedList;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySys.Controllers
{
    public class ReaderController : Controller
    {
        // GET: Reader
        LibSysEntities db = new LibSysEntities();
        public ActionResult Index(string s, int page = 1)
        {

            var index = from b in db.Readers.Where(x => x.Activate ==true) select b;
            if (!string.IsNullOrEmpty(s))
            {

                index = index.Where(m => m.FirstName.Contains(s)
                || m.LastName.Contains(s)
                || m.Email.Contains(s)
                || m.School.Contains(s)
                || m.PhoneNumber.Contains(s));

            }
            return View(index.ToList().ToPagedList(page, 6));
        }

        public ActionResult Passive(string s, int page = 1)
        {

            var index = from b in db.Readers.Where(x => x.Activate == false) select b;
            if (!string.IsNullOrEmpty(s))
            {

                index = index.Where(m => m.FirstName.Contains(s)
                || m.LastName.Contains(s)
                || m.Email.Contains(s)
                || m.School.Contains(s)
                || m.PhoneNumber.Contains(s));

            }
            return View(index.ToList().ToPagedList(page, 6));
        }

        public ActionResult ActivateReader(int ID)
        {
            var delete = db.Readers.Find(ID);
            delete.Activate = true;
            // db.Readers.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddReader()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddReader(Reader r)
        {
            if (!ModelState.IsValid)
            {
                return View("AddReader");
            }
            ScryptEncoder encoder = new ScryptEncoder();
            Reader userItems = new Reader();
            string hashedPassword = encoder.Encode("Password");
            userItems.Email = r.Email;
            userItems.Activate = true;
            userItems.School = r.School;
            userItems.PhoneNumber = r.PhoneNumber;
            userItems.LastName = r.LastName;
            userItems.FirstName = r.FirstName;
            userItems.Password = hashedPassword;
            db.Readers.Add(userItems);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DeleteReader(int ID)
        {
            var delete = db.Readers.Find(ID);
            delete.Activate = false;
            // db.Readers.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateReaderPage(int ID)
        {
            List<SelectListItem> activate = new List<SelectListItem>() {
        new SelectListItem { Text = "Активирай", Value = "true" },new SelectListItem {   Text = "Деакнивирай", Value = "false" }};
            ViewBag.act = activate;
            var update = db.Readers.Find(ID);
            return View("UpdateReaderPage", update);
        }

        public ActionResult UpdateReader(Reader r)
        {

            ScryptEncoder encoder = new ScryptEncoder();

          // var user = (string)Session["Email"];
            var userItems = db.Readers.Find(r.ID_reader);
            // bool areEquals = encoder.Compare("Password", hashPassword.ToString());
            //Reader userItems = new Reader();
            string hashedPassword = encoder.Encode("Password");
            userItems.School = r.School;
            userItems.PhoneNumber = r.PhoneNumber;
            userItems.LastName = r.LastName;
            userItems.FirstName = r.FirstName;
            userItems.Password = hashedPassword;

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Archive(int ID)
        {
            var book = db.ReaderBorrows.Where(x => x.ID_reader == ID).ToList();
            var reader = db.Readers.Where(y => y.ID_reader == ID).Select(z => z.FirstName + " " + z.LastName).FirstOrDefault();
            ViewBag.r = reader;
            return View(book);
        }
    }
}