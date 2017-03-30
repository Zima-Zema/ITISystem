using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITISystem.Models;

namespace ITISystem.Controllers
{
    public class CourseController : Controller
    {
        ApplicationDbContext iti = new ApplicationDbContext();


        // GET: Course
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
        { if (ModelState.IsValid)
            {
                iti.Courses.Add(crs);
                iti.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(crs);
            }


            // return Content("Data added Successfully");
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
        public ActionResult DeleteCourse(int Id, Course crs)

        {
            Course crss = iti.Courses.FirstOrDefault(cs => cs.Course_Id == crs.Course_Id);
            iti.Courses.Remove(crss);
            iti.SaveChanges();
            return RedirectToAction("Index", iti.Courses);
        }

        //b)
        public ActionResult crs_dept()

        {
            ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name", 1);


            return View();
        }
        [HttpGet]
        public ActionResult Courses(int id)
        {
            //Department dept = iti.Departments.First(d => d.Department_Id == id);
            List<Course> Courses = iti.DeptS_CrS_InstrS.Where(c => c.Department_key != id).Select(d => d.Courses).ToList();
            // List<Course> Courses = iti.Courses.Select(c => c).ToList();

            //Dept_Crs_Instr dept = new Dept_Crs_Instr();

            return View(Courses);
        }



        [HttpPost]
        public ActionResult Courses(Course crs, Course[] crslist)
        {
          
            if (ModelState.IsValid == true)
            {
               foreach(var ids in crslist)
                {
                    // int crsId= crs.Course_Id;

                    // crs.CrsDeptInstr.Add();
               
                   crs.CrsDeptInstr.SingleOrDefault(n => n.Courses == ids);
                    //var dprt_id = iti.Courses.FirstOrDefault(c => c.Course_Id == ids);
                    iti.SaveChanges();
                }

             
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }


        // C)
        public ActionResult crs_ins()

        {
            ViewBag.dpts = new SelectList(iti.Instructor, "Instructor_Id", "Name", 2);


            return View();
        }
        [HttpGet]
        public ActionResult ins_courses(int id)
        {
           
            List<Course> Courses = iti.DeptS_CrS_InstrS.Where(c => c.Instructor_key != id).Select(d => d.Courses).ToList();
           
            return View(Courses);
        }


    }



}
    