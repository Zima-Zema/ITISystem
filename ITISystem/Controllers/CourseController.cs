﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITISystem.Models;
using System.Web.Script.Serialization;
using ITISystem.ViewModel;
using System.Data.Entity.Validation;

namespace ITISystem.Controllers
{
    public class CourseController : Controller
    {
        ApplicationDbContext iti = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(iti.Courses);
        }
        [HttpGet]
        public ActionResult Addcourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Addcourse(Course crs)
        {

            if (ModelState.IsValid)
            {
                var coursesNames = iti.Courses.Where(a => a.Name.ToLower() == crs.Name.ToLower()).Select(a => a).FirstOrDefault();
                if (coursesNames == null)
                {
                    iti.Courses.Add(crs);

                    try
                    {
                        iti.SaveChanges();
                        return RedirectToAction("Index");
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

                else
                {
                    ViewData["message"] = "This Course is exist";
                    return View(crs);
                }
            }
            else
            {
                return View(crs);
            }



        }
        [HttpGet]
        public ActionResult EditCourse(int Id)
        {
            Course crs = iti.Courses.FirstOrDefault(cr => cr.Course_Id == Id);
            return PartialView(crs);
        }
        [HttpPost]
        public ActionResult EditCourse(Course crs)
        {
            Course crsold = iti.Courses.FirstOrDefault(cs => cs.Course_Id == crs.Course_Id);
            crsold.Name = crs.Name;
            crsold.Lec_Duration = crs.Lec_Duration;
            crsold.Lab_Duration = crs.Lab_Duration;
            crsold.Status = crs.Status;
            crsold.Duration = crs.Duration;
            iti.SaveChanges();
            return RedirectToAction("Index", iti.Courses);
        }
        [HttpGet]
        public ActionResult DeleteCourse(int Id)
        {
            Course crs = iti.Courses.FirstOrDefault(cr => cr.Course_Id == Id);
            return PartialView(crs);
        }

        [HttpPost]
        public ActionResult DeleteCourse(int Id, int? page)
        {
            
        
            Course crs = iti.Courses.Where(a => a.Course_Id == Id).FirstOrDefault();
            int asd = 0;
          

            Std_Crs_Instr std_cors = iti.StdS_CrS_InstrS.Where(a => a.Course_key == crs.Course_Id).FirstOrDefault();
            Dept_Crs_Instr dept_crs
                = iti.DeptS_CrS_InstrS.Where(a => a.Course_key == crs.Course_Id).FirstOrDefault();
            if (crs != null)
            {
                asd++;
                if (asd == 1)
                {
                    try
                    {

                        if (/*crs.Status == CourseStatus.Finish ||*/ std_cors == null || crs.Departments.Count == 0)
                        {
                            iti.Courses.Remove(crs);
                            iti.SaveChanges();
                        }

                        else
                        {
                            ViewData["message"] = "you can't delete this course";
                            // return View(crs);
                        }


                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return RedirectToAction("index", iti.Courses);

        }

        //b)

        [HttpGet]
        public ActionResult crs_dept()
        {
            List<Department> depts = iti.Departments.Select(d => d).ToList();
            return View(depts);
        }

        [HttpPost]
        public ActionResult crs_dept(int id)
        {
            List<Course> course = new List<Course>();
            var crs = iti.Courses.Select(c => c).ToList();
            List<Course> crs2 = iti.Departments.Single(c => c.Department_Id == id).Courses.ToList();
            foreach (var item in crs)
            {
                if (crs2.FirstOrDefault(k => k.Course_Id == item.Course_Id) == null)
                {
                    course.Add(new Course() { Course_Id = item.Course_Id, Name = item.Name });
                }

            }
            iti.Configuration.ProxyCreationEnabled = false;

            var cur = new JavaScriptSerializer();

            var obj = cur.Serialize(course);
            if (course.Count == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public void AddCoursesToDept(int Department_Id, int Course_Id)
        {
            var cors = iti.Courses.SingleOrDefault(c => c.Course_Id == Course_Id);
            List<Course> crs2 = iti.Departments.Single(c => c.Department_Id == Department_Id).Courses.ToList();
            Department dept = iti.Departments.SingleOrDefault(d => d.Department_Id == Department_Id);
            dept.Courses.Add(cors);
            iti.SaveChanges();

        }
        /// <summary>
        /// //////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult del_crs_dept()
        {
            List<Department> dept = iti.Departments.Select(d => d).ToList();
            return View(dept);
        }



        [HttpPost]
        public ActionResult del_crs_dept(int id)
        {
            List<Course> course = new List<Course>();
            // var crs = iti.Courses.Select(c => c).ToList();
            List<Course> crs2 = iti.Departments.Single(c => c.Department_Id == id).Courses.ToList();
            foreach (var item in crs2)
            {
                if (crs2.FirstOrDefault(k => k.Course_Id == item.Course_Id) != null)
                {
                    course.Add(new Course() { Course_Id = item.Course_Id, Name = item.Name });
                }

            }
            iti.Configuration.ProxyCreationEnabled = false;

            var cur = new JavaScriptSerializer();

            var obj = cur.Serialize(course);
            if (course.Count == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public void DeleteCrsFromDept(int Department_Id, int Course_Id)
        {
            var cors = iti.Courses.SingleOrDefault(c => c.Course_Id == Course_Id);
            var crss = iti.Departments.Single(c => c.Department_Id == Department_Id).Courses.ToList();
            Department dept = iti.Departments.SingleOrDefault(d => d.Department_Id == Department_Id);
            dept.Courses.Remove(cors);
            iti.SaveChanges();

        }




        //c)

        [HttpGet]
        public ActionResult crs_ins()
        {
            List<Instructor> ins = iti.Instructor.Select(d => d).ToList();
            return View(ins);
        }
        [HttpPost]
        public ActionResult crs_ins(int id)
        {
            List<Course> course = new List<Course>();
            var crs = iti.Courses.Select(c => c).ToList();
            List<Course> crs2 = iti.Instructor.Single(c => c.Instructor_Id == id).Courses.ToList();

            foreach (var item in crs)
            {
                if (crs2.FirstOrDefault(k => k.Course_Id == item.Course_Id) == null)
                {
                    course.Add(new Course() { Course_Id = item.Course_Id, Name = item.Name });
                }

            }
            iti.Configuration.ProxyCreationEnabled = false;

            var cur = new JavaScriptSerializer();

            var obj = cur.Serialize(course);
            if (course.Count == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

        }
        public void AddCoursesToIns(int Instructor_Id, int Course_Id)
        {
            var crr = iti.Courses.SingleOrDefault(c => c.Course_Id == Course_Id);
            List<Course> crs2 = iti.Instructor.Single(c => c.Instructor_Id == Instructor_Id).Courses.ToList();
            Instructor ins = iti.Instructor.SingleOrDefault(d => d.Instructor_Id == Instructor_Id);
            ins.Courses.Add(crr);
            iti.SaveChanges();

        }
        /// <summary>
        /// ///////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult del_crs_ins()
        {
            List<Instructor> ins = iti.Instructor.Select(d => d).ToList();
            return View(ins);
        }

        [HttpPost]
        public ActionResult del_crs_ins(int id)
        {
            List<Course> course = new List<Course>();
            // var crs = iti.Courses.Select(c => c).ToList();
            List<Course> crs2 = iti.Instructor.Single(c => c.Instructor_Id == id).Courses.ToList();
            foreach (var item in crs2)
            {
                if (crs2.FirstOrDefault(k => k.Course_Id == item.Course_Id) != null)
                {
                    course.Add(new Course() { Course_Id = item.Course_Id, Name = item.Name });
                }

            }
            iti.Configuration.ProxyCreationEnabled = false;

            var cur = new JavaScriptSerializer();

            var obj = cur.Serialize(course);
            if (course.Count == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public void DeleteCrsFromIns(int Instructor_Id, int Course_Id)
        {
            var cors = iti.Courses.SingleOrDefault(c => c.Course_Id == Course_Id);
            var crss = iti.Instructor.Single(c => c.Instructor_Id == Instructor_Id).Courses.ToList();
            Instructor ins = iti.Instructor.SingleOrDefault(d => d.Instructor_Id == Instructor_Id);
            ins.Courses.Remove(cors);
            iti.SaveChanges();

        }



        /// </summary>
        /// <returns></returns>
        /// 


        //D
        [HttpGet]
        public ActionResult AssignInstructor()
        {
            try
            {
                var depts = iti.Departments.Select(a => a);
                ViewBag.depts = new SelectList(depts, "Department_Id", "Name");

                return View();
            }

            catch { return RedirectToAction("Index"); }
        }
        [HttpGet]
        public ActionResult courses(int id)
        {

            try
            {
                var crs_list = iti.Departments.Where(a => a.Department_Id == id).SelectMany(a => a.Courses).ToList();
                TempData["dept_id"] = id;
                ViewBag.crs = new SelectList(crs_list, "Course_id", "Name");
                var insts = iti.Instructor.Select(a => a).ToList();
                ViewBag.inst = new SelectList(insts, "Instructor_Id", "Name");
                return PartialView();
            }
            catch { return RedirectToAction("Index"); }
        }


        [HttpPost]
        public ActionResult AssignInstructor(Dept_Crs_Instr item)
        {
            try
            {
                var dept_id = int.Parse(TempData["dept_id"].ToString());
                item.Department_key = dept_id;

                iti.DeptS_CrS_InstrS.Add(item);
                iti.SaveChanges();

                return RedirectToAction("Index");
            }

            catch { return RedirectToAction("Index"); }
        }
    }




}
