using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ITISystem.Controllers
{
    public class StudentController : Controller
    {
        ApplicationDbContext iti = new ApplicationDbContext();
        // GET: Student
        public ActionResult Index()
        {
            var models = iti.Students.Include(ss => ss.Department);
            return View(models);
        }
        [HttpGet]
        public ActionResult Create_Student()
        {
            ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create_Student(Student std)
        {
            int? selected_dept = std.Department_Key;
            int cap = iti.Departments.Single(d => d.Department_Id == selected_dept).Capacity;
            int count_std = iti.Students.Where(s => s.Department_Key == selected_dept).Count();
            bool email_check = iti.Students.Any(s => s.Email == std.Email);
            if (count_std < cap && email_check == false)
            {
                //if (ModelState.IsValid)
                //{
                iti.Students.Add(std);
                try
                {
                    iti.SaveChanges();
                    return RedirectToAction("index");
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }
            else if (count_std > cap)
            {
                std.Department_Key = null;
                iti.Students.Add(std);
                iti.SaveChanges();
                return RedirectToAction("index");
            }
            else if (email_check == true)
            {
                ViewData["message"] = "This Email Exists Before!!!";
                ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
                return View(std);
            }
            else
            {
                ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
                return View(std);
            }
        }
        //Department
        [HttpGet]
        public ActionResult Mange_NoDepts()
        {
            ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
            var std_no= iti.Students.Where(s => s.Department_Key == null).ToList();
            SelectList stds = new SelectList(std_no, "Student_id", "FirstName");
            //
            ViewData["stds"] = stds;
            return View();
        }
        [HttpPost]
        public ActionResult Mange_NoDepts(Student std)
        {
            //ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
            if (std.FirstName != null)
            { }
            return RedirectToAction("index");
        }
    }
}
