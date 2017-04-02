using ITISystem.Models;
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
            Instructor ins = iti.Instructor.FirstOrDefault(i => i.Instructor_Id == inst.Instructor_Id);
            iti.Instructor.Remove(ins);
            iti.SaveChanges();
            return RedirectToAction("getAll", iti.Instructor);

        }
        //[HttpGet]
        //public ActionResult Instructor_Courses()
        //{
        //    ViewBag.ins = new SelectList(iti.Instructor, "Instructor_id", "Name");
        //    return View();

        //}

        public ActionResult Finished_Courses(int id)
        {
            var courseList = iti.StdS_CrS_InstrS.Where(i => i.Instructor_key == id).Select(i => i.Courses).ToList();
            var finish = courseList.Where(c => c.Status == CourseStatus.Finish).ToList();
            return View(finish);

        }

        //public ActionResult StudentDegreeByInstructor(int insID, int deptID, int crsID)
        //{
        //    var studentList = iti.DeptS_CrS_InstrS.Where(i => i.Course_key == crsID && i.Instructor_key == insID && i.Department_key == deptID).Select(i => i.);
        //}

        public ActionResult ManagerID()
        {
            return View();

        }

        public ActionResult DisplayEvaluationOfInstructor(int id)
        {
            var mngDept = iti.Departments.Where(d => d.manger_key == id).FirstOrDefault(d => d.Department_Id.HasValue);
            var InsEva = iti.DeptS_CrS_InstrS.Where(i => i.Department_key == mngDept.Department_Id).ToList();
            return View(InsEva);
        }



        public ActionResult Add_Degree()
        {
            var dept_Id = iti.Instructor.Single(a => a.Instructor_Id == 2).Department_Key;
            ViewBag.dept_name = iti.Departments.Single(a=>a.Department_Id==dept_Id).Name;
            ViewBag.course = new SelectList(iti.Courses, "Course_Id", "Name");
            ViewBag.students = new SelectList(iti.Students.Where(a=>a.Department_Key==dept_Id), "Student_Id", "FirstName").ToList();
            
            return View();
        }
     //   [HttpPost]
        //public ActionResult Add_Degree(InstructorViewModel inst)
        //{

        //    int course_id = inst.Courses.Course_Id;
        //    Std_Crs_Instr std_degree = inst.items;
        //    //int ins_id= inst.Instructors.Instructor_Id;
        //    //int course_id = inst.Courses.Course_Id;
        //    foreach (var item in std_degree)
        //    {
        //        iti.StdS_CrS_InstrS.Add(std_degree);
        //        iti.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}
        //[HttpGet]
        
        [HttpGet]
        public ActionResult manger_student()
        {
               List<Department> depts = iti.Departments.Select(d => d).ToList();
            return View(depts);

        }
        [HttpPost]
        public ActionResult manger_student(int id)
        {
            List<Student> students = iti.Students.Where(ss => ss.Department_Key == id).ToList();
            iti.Configuration.ProxyCreationEnabled = false;

            var cur = new JavaScriptSerializer();

            var obj = cur.Serialize(students);
            if (students.Count == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }


    }
}