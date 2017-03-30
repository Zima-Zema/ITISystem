using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ITISystem.ViewModel;

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
            else if (count_std > cap)
            {
                std.Department_Key = null;
                iti.Students.Add(std);
                iti.SaveChanges();
                return RedirectToAction("Index");
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
            try
            {
                ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
                var std_no = iti.Students.Where(s => s.Department_Key == null).ToList();
                ViewBag.stds = new SelectList(std_no, "Student_Id", "FirstName");
                //
                // ViewData["stds"] = stds;
                return View();
            }
            catch { return RedirectToAction("Index"); }
        }
        [HttpPost]
        public ActionResult Mange_NoDepts(Student std,Department dpt,bool chk)
        {
            if(chk==true)
            {
                //int? selected_dept = std.Department_Key;
                var std_dpt = std.Department_Key;
                int cap = iti.Departments.Single(d => d.Department_Id == std_dpt).Capacity;
                int count_std = iti.Students.Where(s => s.Department_Key == std_dpt).Count();
                if (count_std <= cap)
                {
                    //var stdddd = std.Student_Id;
                    var ss = iti.Students.Single(s => s.Student_Id == std.Student_Id);
                    ss.Department_Key = std_dpt;
                    iti.SaveChanges();
                    return RedirectToAction("Index");
                }
                else if (count_std > cap)
                {
                    var ss = iti.Students.Single(s => s.Student_Id == std.Student_Id);
                    ss.Department_Key = null;
                    iti.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    return View(std);
                }
            }
            else {
                //try
                //{                
                    return RedirectToAction("index");
                    //return View(std);
                //}
                //catch
                //{
                //    return RedirectToAction("index");
                //}
                }         
        }
        //course
        [HttpGet]
        public ActionResult crs_grade(int id)
        {
            try
            {
                var std_id = iti.StdS_CrS_InstrS.Where(s => s.Student_key == id).ToList();
                return View(std_id);
            }
            catch
            {
                return RedirectToAction("Index");
            }
       }
        public ActionResult details(int id)
        {
            var std_id = iti.Students.Single(s => s.Student_Id == id);
            return View(std_id);
        }
        [HttpGet]
        public ActionResult evaluation()
        {
            ViewBag.stds = new SelectList(iti.Students, "Student_Id", "FirstName");
           // var crs_std=iti.StdS_CrS_InstrS.Where()
            ViewBag.crs = new SelectList(iti.Courses, "Course_id", "Name");
            ViewBag.inst = new SelectList(iti.Instructor, "Instructor_Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult evaluation(Course crs)
        {
            return View();
        }
        public ActionResult Go_Back()
        {
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var viewModel2 = new StudentViewModel()
            {
                DepartmentList = iti.Departments.ToList(),
                StudentList = iti.Students.ToList(),
                Student = iti.Students.SingleOrDefault(a => a.Student_Id == Id)
            };
            return PartialView(viewModel2);
        }
        [HttpPost]
        public ActionResult Edit(ViewModel.StudentViewModel st)
        {
            Student std_new = st.Student;
            Student std_old = iti.Students.SingleOrDefault(a => a.Student_Id == std_new.Student_Id);
            std_old.FirstName = std_new.FirstName;
            std_old.LastName = std_new.LastName;
            std_old.Password = std_new.Password;
            std_old.BirthDate = std_new.BirthDate;
            std_old.Email = std_new.Email;
            std_old.UserName = std_new.UserName;
            std_old.Attend_Balance = std_new.Attend_Balance;
            std_old.Telephone = std_new.Telephone;
            std_old.Address.City = std_new.Address.City;
            std_old.Address.Country = std_new.Address.Country;
            std_old.Address.Street = std_new.Address.Street;
            std_old.Department_Key = std_new.Department_Key;
            iti.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            Student std = iti.Students.SingleOrDefault(a => a.Student_Id == Id);
            return PartialView(std);
        }
        [HttpPost]
        public ActionResult Delete(int Id,Student std)
        {
            var removeStd = iti.Students.SingleOrDefault(s => s.Student_Id == Id);
            iti.Students.Remove(removeStd);
            iti.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
