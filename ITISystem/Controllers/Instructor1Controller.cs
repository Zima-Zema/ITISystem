﻿using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Data.Entity;
using ITISystem.ViewModel;
using System.Web.Script.Serialization;

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
            var models = iti.Instructor.Include(ss => ss.Department);
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
        public ActionResult Remove(int id, Instructor inst)
        {
            Instructor ins = iti.Instructor.SingleOrDefault(i => i.Instructor_Id == id);
            iti.Instructor.Remove(ins);
            iti.SaveChanges();
            return RedirectToAction("getAll", iti.Instructor);

        }
       
        public ActionResult Finished_Courses(int id)
        {
            var courseList = iti.StdS_CrS_InstrS.Where(i => i.Instructor_key == id).Select(i => i.Courses).ToList();
            var finish = courseList.Where(c => c.Status == CourseStatus.Finish).ToList();
            return View(finish);

        }

  

        public ActionResult DisplayEvaluationOfInstructor(int id)
        {
            var mngDept = iti.Departments.Where(d => d.manger_key == id).FirstOrDefault(d => d.Department_Id.HasValue);
            var InsEva = iti.DeptS_CrS_InstrS.Where(i => i.Department_key == mngDept.Department_Id).Include(ss => ss.Instructors).ToList();
            return View(InsEva);
            
        }



        //Add_Degree

        [HttpGet]
        public ActionResult Add_Degree(int id)
        {

            TempData["inst_id"] = id;
            var dept_id = iti.Instructor.Single(a => a.Instructor_Id == id).Department_Key;
            var courses = iti.DeptS_CrS_InstrS.Where(a => a.Department_key == dept_id&&a.Instructor_key==id).Select(a=>a.Courses);
            var students = iti.Students.Where(a => a.Department_Key == dept_id);
            ViewBag.stds = new SelectList(students, "Student_Id", "FirstName");
            ViewBag.cors = new SelectList(courses, "Course_Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Add_Degree(Std_Crs_Instr item)
        {
            var inst_id = int.Parse(TempData["inst_id"].ToString());
            item.Instructor_key = inst_id;
            iti.StdS_CrS_InstrS.Add(item);
            iti.SaveChanges();
            return View("getAll");
        }
        //[HttpGet]
        //public ActionResult Departs(int id)
        //{
        //    var depts = iti.DeptS_CrS_InstrS.Where(a => a.Course_key == id).Select(a => a.Departments);
        //    ViewBag.depts = new SelectList(depts, "Department_Id", "Name");
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult StudentsDegree(int id)
        //{
        //    var students = iti.Students.Where(a=>a.Department_Key==id);
        //    ViewBag.stds = new SelectList(students, "Student_Id", "FirstName");
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Add_Degree(InstructorViewModel inst)
        //{

        //    int course_id = inst.Courses.Course_Id;
        //    Std_Crs_Instr std_degree = inst.items;
        //    //int ins_id= inst.Instructors.Instructor_Id;
        //    //int course_id = inst.Courses.Course_Id;
        //    foreach (var item in inst)
        //    {
        //        iti.StdS_CrS_InstrS.Add(std_degree);
        //        iti.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}
        //[HttpGet]
        
        [HttpGet]
        public ActionResult crs_gradeForStudent(int id)
        {
            try
            {
                var std_id = iti.StdS_CrS_InstrS.Where(s => s.Student_key == id).ToList();
                return View(std_id);
            }
            catch
            {
                return RedirectToAction("ManageStudent");
            }
        }

        public ActionResult ManageStudent( int id)
        {
            var mngstud = iti.Departments.Where(d => d.manger_key == id).FirstOrDefault(d => d.Department_Id.HasValue);
            var allstud = iti.Students.Where(s => s.Department_Key == mngstud.Department_Id);
            return View(allstud);
        }

        //give_premission
        

        public ActionResult Give_Premision(int Id)
        {
            TempData["inst_id"] = Id;
            var dept_id = iti.Instructor.Single(a => a.Instructor_Id == Id).Department_Key;
            var students = iti.Students.Where(a => a.Department_Key == dept_id);
            ViewBag.stds = new SelectList(students, "Student_Id", "FirstName");


            return View();
        }
     
          
        [HttpPost]
        public ActionResult Give_Premision(Permisions per_std,bool chk)
        {
            var ins_id = int.Parse(TempData["inst_id"].ToString());
            
            if (chk == true)
            {
                per_std.Instructor_key = ins_id;
                per_std.Type = premission.allow;
            }
            else {
                per_std.Instructor_key = ins_id;
                per_std.Type = premission.reject;
            }
            iti.Premissions.Add(per_std);
            iti.SaveChanges();
            return RedirectToAction("getAll");
        }
    }
}