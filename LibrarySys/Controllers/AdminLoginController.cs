using LibrarySys.Models.Entity;
using LibrarySys.Sect;
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
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        LibSysEntities db = new LibSysEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Employee e)
        {

            ScryptEncoder encoder = new ScryptEncoder();
            var x = db.Employees.Where(a => a.Email.Equals(e.Email)).First();
            bool areEquals = encoder.Compare("Password", x.Password.ToString());
            var dataItem = db.Employees.Where(a => a.Email.Equals(e.Email) && areEquals == true).First();

            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.Email, false);
                Session["Email"] = dataItem.Email.ToString();
                Session["FisrtName"] = dataItem.FirstName.ToString();
                Session["LastName"] = dataItem.LastName.ToString();
               

                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                ModelState.AddModelError("Login", "Грешна парола или имейл. Опитайте отново.");
                return View();
            }

            
           
        }
    }
}