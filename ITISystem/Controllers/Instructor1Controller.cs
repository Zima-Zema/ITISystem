using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Data.Entity;


namespace ITISystem.Controllers
{
    public class Instructor1Controller : Controller
    {
        ApplicationDbContext iti = new ApplicationDbContext();
        // GET: Instructor1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getAll()
        {
            var models = iti.Instructor.Include(ss=>ss.Department);
            return View(models);
        }
        [HttpGet]
        public ActionResult createInstructor()
        {
            ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
            
            return View();

        }
        [HttpPost]
        public ActionResult createInstructor(Instructor ins)
        {

            if (ModelState.IsValid)
            {
                iti.Instructor.Add(ins);
                iti.SaveChanges();
                return RedirectToAction("getAll");
            }
            else
            {
                return View(iti.Instructor);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            Instructor ins = iti.Instructor.FirstOrDefault(i => i.Instructor_Id == id);
            ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");

            return PartialView(ins);

        }

        [HttpPost]
        public ActionResult Edit(Instructor ins)
        {

           var Oldins = iti.Instructor.FirstOrDefault(i => i.Instructor_Id == ins.Instructor_Id);
            Oldins.Name = ins.Name;
            Oldins.BirthDate = ins.BirthDate;
            Oldins.Degree = ins.Degree;
            //Oldins.Department = ins.Department;
            Oldins.Department_Key = ins.Department_Key;
            Oldins.Graduation_Year = ins.Graduation_Year;
            Oldins.Work_Status = ins.Work_Status;

            iti.SaveChanges();
            return RedirectToAction("getAll", iti.Instructor);

        }
        [HttpGet]
        public ActionResult Remove(int id)
        {
            Instructor ins = iti.Instructor.FirstOrDefault(i => i.Instructor_Id == id);

            return PartialView(ins);

        }

        [HttpPost]
        public ActionResult Remove(int id,Instructor inst)
        {
            Instructor ins = iti.Instructor.FirstOrDefault(i => i.Instructor_Id == inst.Instructor_Id);
            iti.Instructor.Remove(ins);
            iti.SaveChanges();
            return RedirectToAction("getAll",iti.Instructor);
            
        }



    }
}