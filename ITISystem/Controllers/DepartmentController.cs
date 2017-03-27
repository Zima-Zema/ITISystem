using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;


namespace ITISystem.Controllers
{
    public class DepartmentController : Controller
    {
        ApplicationDbContext iti = new ApplicationDbContext();
        // GET: Department
        public ActionResult Index()
        {
            var depts = iti.Departments.Include(dd => dd.instructor_mang);
            return View(depts.ToList());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var depts = iti.Departments.Include(dd => dd.instructor_mang).Where(s => s.Department_Id == id).Single();
            var ins = iti.Instructors.ToList();
            ViewBag.ins = new SelectList(ins, "Instructor_Id", "Name", depts.manger_key);
            return PartialView(depts);
        }
        [HttpPost]
        public ActionResult Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                var deptsold = iti.Departments.Include(dd => dd.instructor_mang).Where(s => s.Department_Id == dept.Department_Id).Single();
                deptsold.Name = dept.Name;
                deptsold.Capacity = dept.Capacity;
                deptsold.manger_key = dept.manger_key;
                iti.SaveChanges();
                
            }
            var depts = iti.Departments.Include(dd => dd.instructor_mang).ToList();

            return View("Index", depts);

        }

    }
}