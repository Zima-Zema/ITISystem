using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITISystem.Models;
using System.Web.Script.Serialization;
using ITISystem.ViewModel;

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
        {

            if (ModelState.IsValid)
            {
                var coursesNames = iti.Courses.Where(a => a.Name.ToLower() == crs.Name.ToLower()).Select(a => a).FirstOrDefault();
                if (coursesNames == null)
                {
                    iti.Courses.Add(crs);
                    iti.SaveChanges();
                    return RedirectToAction("Index");
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
                    iti.SaveChanges();

                    return RedirectToAction("Index", iti.Courses);
                }
                else
                {
                    ViewData["message"] = "you can't delete this course";

                    return View(crs); 
                }
            

            

        }
    
        //b)
        //view to show dropdownlist for departments
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
           

            List<Course> crs2 = iti.DeptS_CrS_InstrS.Where(d => d.Department_key == id).Select(d => d.Courses).ToList();
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
            var cors = iti.Courses.FirstOrDefault(c => c.Course_Id == Course_Id);
            List<Course> crs2 = iti.DeptS_CrS_InstrS.Where(d => d.Department_key == Department_Id).Select(d => d.Courses).ToList();

        

            Dept_Crs_Instr deptcourse = new Dept_Crs_Instr() { Course_key = Course_Id, Department_key = Department_Id };
            Dept_Crs_Instr deptcourseexitornoo = iti.DeptS_CrS_InstrS.Where(a => a.Course_key == deptcourse.Course_key && a.Department_key == deptcourse.Department_key).Select(a => a).SingleOrDefault();
              if (deptcourseexitornoo == null)
            {
                iti.DeptS_CrS_InstrS.Add(deptcourse);

                iti.SaveChanges();
            }




           
        }










        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        //c)

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
    