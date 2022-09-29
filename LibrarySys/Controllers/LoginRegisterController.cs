using LibrarySys.Models.Entity;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LibrarySys.Controllers
{
    [AllowAnonymous]
    public class LoginRegisterController : Controller
    {

        // GET: LoginRegister
        LibSysEntities db = new LibSysEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Reader r)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            var x = db.Readers.Where(a => a.Email.Equals(r.Email)).First();
            bool areEquals = encoder.Compare("Password", x.Password.ToString());
            var dataItem = db.Readers.Where(a => a.Email.Equals(r.Email) && areEquals == true).First();

            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.Email, false);
                Session["Email"] = dataItem.Email.ToString();
                Session["FisrtName"] = dataItem.FirstName.ToString();
                Session["LastName"] = dataItem.LastName.ToString();
                Session["School"] = dataItem.School.ToString();
                return RedirectToAction("Index", "ReaderPanel");
            }
            else
            {
                ModelState.AddModelError("Login", "Грешна парола или имейл. Опитайте отново.");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Reader r)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (ModelState.IsValid)
            {
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
                return RedirectToAction("Login");
            }
            return View("Register");
        }


    }
}