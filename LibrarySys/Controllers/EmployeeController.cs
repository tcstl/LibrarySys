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
    public class EmployeeController : Controller
    {
        // GET: Employee
        LibSysEntities db = new LibSysEntities();
        public ActionResult Index(string s, int page = 1)
        {

            var index = from b in db.Employees select b;
            if (!string.IsNullOrEmpty(s))
            {

                index = index.Where(m => m.FirstName.Contains(s)
                                       || m.LastName.Contains(s)
                                       ||  m.Email.Contains(s)
                                       ||  m.Adress.Contains(s)
                );

            }
            return View(index.ToList().ToPagedList(page, 6));
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            List<SelectListItem> r = new List<SelectListItem>() {
        new SelectListItem { Text = "Aktive", Value = "A" },new SelectListItem {   Text = "Pasive", Value = "B" }};
            ViewBag.role = r;

            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (ModelState.IsValid)
            {
                Employee userItems = new Employee();
                string hashedPassword = encoder.Encode("Password");
                userItems.Email = employee.Email;
                userItems.LastName = employee.LastName;
                userItems.FirstName = employee.FirstName;
                userItems.Password = hashedPassword;
                userItems.Role= employee.Role;
                userItems.Adress = employee.Adress;
                db.Employees.Add(userItems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddEmployee");
            
        }

        public ActionResult DeleteEmployee(int ID)
        {
            var delete = db.Employees.Find(ID);
            db.Employees.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateEmployeePage(int ID)
        {
            var update = db.Employees.Find(ID);
            List<SelectListItem> r = new List<SelectListItem>() {
        new SelectListItem { Text = "Aktive", Value = "A" },new SelectListItem {   Text = "Pasive", Value = "B" }};
            ViewBag.role = r;
            return View("UpdateEmployeePage", update);
        }

        public ActionResult UpdateEmployee(Employee employee)
        {
            var emp = db.Employees.Find(employee.ID_employee);
            emp.Role = employee.Role;
            emp.FirstName = employee.FirstName;
            emp.LastName = employee.LastName;
            emp.Email = employee.Email;
            emp.Password = employee.Password;
            emp.Adress = employee.Adress;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}