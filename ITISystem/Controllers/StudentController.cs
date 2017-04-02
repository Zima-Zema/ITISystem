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
            var models = iti.Students.OrderBy(ss=>ss.FirstName).ThenBy(ss=>ss.LastName).Include(ss => ss.Department);
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
            int real_cap = cap - 1;
            int count_std = iti.Students.Where(s => s.Department_Key == selected_dept).Count();
            bool email_check = iti.Students.Any(s => s.Email == std.Email);
            bool username_check = iti.Students.Any(s => s.UserName == std.UserName);
            if (count_std <= real_cap && email_check == false && username_check==false)
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
            if (email_check == true)
            {
                ViewData["message"] = "This Email Exists Before!!!";
                ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
                return View(std);
            }
            if (username_check == true)
            {
                ViewData["message_username"] = "Please Change User Name, It Exists Before.";
                ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
                return View(std);
            }
            if (count_std > real_cap)
            {
                std.Department_Key = null;
                iti.Students.Add(std);
                iti.SaveChanges();
                return RedirectToAction("Index");
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
               // var full_n = iti.Students.Where(s => s.Department_Key == null).Select(s => new { full_name = s.FirstName + " "+ s.LastName });
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
                int real_cap = cap - 1;
                int count_std = iti.Students.Where(s => s.Department_Key == std_dpt).Count();
                if (count_std <= real_cap)
                {
                    var ss = iti.Students.Single(s => s.Student_Id == std.Student_Id);
                    ss.Department_Key = std_dpt;
                    iti.SaveChanges();
                    return RedirectToAction("Index");
                }
                else if (count_std > real_cap)
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
            return View();
        }
        [HttpGet]
        public ActionResult course_eval(int id)
        {
            var crs_list = iti.StdS_CrS_InstrS.Where(s => s.Student_key == id).Select(c => c.Courses);
            ViewBag.crs = new SelectList(crs_list, "Course_id", "Name");
            TempData["Id_Instructor"] = id;
            return View();
        }
        [HttpGet]
        public ActionResult instructor_eval(int id)
        {
            var student_id = TempData["Id_Instructor"].ToString();
            var instr_list = iti.StdS_CrS_InstrS.Where(s => s.Course_key == id && s.Student_key.ToString() == student_id).Select(c => c.Instructors).ToList();
            ViewBag.inst = new SelectList(instr_list, "Instructor_id", "Name");
            TempData["Course_Id"] = id;
            TempData["Identity_Course"] = id;
            TempData["Student_Id"] = student_id;
            TempData["Identity_Student"] = student_id;
            var instr_id = iti.StdS_CrS_InstrS.Where(s => s.Course_key == id && s.Student_key.ToString() == student_id).Select(c => c.Instructor_key).FirstOrDefault();
            TempData["Instructor_Id"] = instr_id;
            TempData["Identity_Instructor"] = instr_id;
            return View();
        }       
        [HttpGet]
        public ActionResult calculate_eval(int? id)
        {
            try
            {
                int? crs_eval = id;
                var std_identity = TempData["Student_Id"].ToString();
                var crs_identity = TempData["Course_Id"].ToString();
                var instr_identity = TempData["Instructor_Id"].ToString();
                var sing_std_crs_instr = iti.StdS_CrS_InstrS.Single(s => s.Student_key.ToString() == std_identity && s.Course_key.ToString() == crs_identity && s.Instructor_key.ToString() == instr_identity);
                sing_std_crs_instr.Crs_evaluation = crs_eval;
                iti.SaveChanges();
                return View();                
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult calculate_eval_instructor(int? id)
        {
            try
            {
                int? instr_eval = id;
                var std_iden = TempData["Identity_Student"].ToString();
                var crs_ident = TempData["Identity_Course"].ToString();
                var instr_ident = TempData["Identity_Instructor"].ToString();
                var sin_std_crs_instr = iti.StdS_CrS_InstrS.Single(s => s.Student_key.ToString() == std_iden && s.Course_key.ToString() == crs_ident && s.Instructor_key.ToString() == instr_ident);
                sin_std_crs_instr.Instr_evaluation = instr_eval;
                iti.SaveChanges();
                return View();
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult courses(int id)
        {return View();}
        public ActionResult instr_eval(int id)
        {return View();}
        public ActionResult instr_eval(int id,Instructor instr)
        {return RedirectToAction("Index");}
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
            int? selected_dept = std_new.Department_Key;
            if (selected_dept == null)
            {
                std_new.Department_Key = null;
            }
            else {

                int cap = iti.Departments.Single(d => d.Department_Id == selected_dept).Capacity;
                int real_cap = cap - 1;
                int count_std = iti.Students.Where(s => s.Department_Key == selected_dept).Count();
                if (count_std <= real_cap)
                {
                    std_old.Department_Key = std_new.Department_Key;
                }
                else if (count_std > real_cap)
                {
                    std_new.Department_Key = null;
                }

            }
            //int cap = iti.Departments.Single(d => d.Department_Id == selected_dept).Capacity;
            //int real_cap = cap - 1;
            //int count_std = iti.Students.Where(s => s.Department_Key == selected_dept).Count();
            //Student std_old = iti.Students.SingleOrDefault(a => a.Student_Id == std_new.Student_Id);
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

           

            //if (count_std <= real_cap)
            //{
            //    std_old.Department_Key = std_new.Department_Key;
            //}
            //else if (count_std > real_cap)
            //{
            //    std_new.Department_Key = null;
            //}
            iti.SaveChanges();
            List<Student> stds = iti.Students.Select(s => s).ToList();
            return PartialView("Add", stds);

        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            Student std = iti.Students.SingleOrDefault(a => a.Student_Id == Id);
            return PartialView(std);
        }
        [HttpPost]
        public ActionResult Delete(int Id,ViewModel.StudentViewModel st)
        {
            Student std = st.Student;
            var removeStd = iti.Students.SingleOrDefault(s => s.Student_Id == Id);
            iti.Students.Remove(removeStd);
            iti.SaveChanges();
            List<Student> stds = iti.Students.Select(s => s).ToList();
            return PartialView("Add",stds);
        }
    }
}
