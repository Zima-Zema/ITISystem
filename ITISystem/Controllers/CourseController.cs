using System;
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
                     catch(DbEntityValidationException ex)
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
        public ActionResult DeleteCourse(int Id, Course crs)

        {
            Course crss = iti.Courses.FirstOrDefault(cs => cs.Course_Id == crs.Course_Id);
            
                var studentscourse = iti.StdS_CrS_InstrS.Where(a => a.Course_key == crss.Course_Id).Select(c => c.Students.Student_Id).ToList();
            if (studentscourse == null)
            {
                iti.Courses.Remove(crss);
                try
                {
                    iti.SaveChanges();
                    return RedirectToAction("Index", iti.Courses);
                }
                catch (DbEntityValidationException ex) {
                    var errorMessages = ex.EntityValidationErrors
                           .SelectMany(x => x.ValidationErrors)
                           .Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                }
            }
            //    else if(crss.Status==CourseStatus.Finish)
            //    {
            //        iti.Courses.Remove(crss);
            //        iti.SaveChanges();
            //        return RedirectToAction("Index", iti.Courses);
            //}
            else
            {
                ViewData["message"] = "you can't delete this course";
                return View(crs);
            }
        }
    
        //b)
       
        [HttpGet]
        public ActionResult crs_dept()
        {           
            List<Department> depts = iti.Departments.Select(d => d).ToList();
            return View(depts);
        }

        [HttpPost]
        public ActionResult crs_dept( int id)
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
        //D
        [HttpGet]
        public ActionResult AssignInstructor()
        {
            try
            {
                ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
                ViewBag.course = new SelectList(iti.Courses, "Course_Id", "Name");
                ViewBag.inst = new SelectList(iti.Instructor, "Instructor_Id", "Name");


                return View();
            }

            catch { return RedirectToAction("Index"); }
        }

        [HttpPost]
        public ActionResult AssignInstructor(Dept_Crs_Instr data)
        {
            try
            {

                iti.DeptS_CrS_InstrS.Add(data);
                iti.SaveChanges();

                return RedirectToAction("Index");
            }

            catch { return RedirectToAction("Index"); }
        }
    }



}
    