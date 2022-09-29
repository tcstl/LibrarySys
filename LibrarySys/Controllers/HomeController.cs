using LibrarySys.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySys.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Home
        LibSysEntities db = new LibSysEntities();
        [HttpGet]
        public ActionResult Index()
        {
            var d7 = db.Books.Count();
            ViewBag.d7 = d7;
            var d8 = db.Readers.Count();
            ViewBag.d8 = d8;
            var d11 = db.Employees.Count();
            ViewBag.d11 = d11;
            var d9 = db.ReaderBorrows.Where(x => x.Process == true).Count();
            ViewBag.d9 = d9;

            return View();
        }
    }
}