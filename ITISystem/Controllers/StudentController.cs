using ITISystem.Models;
using ITISystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITISystem.Controllers
{
    public class StudentController : Controller
    {
        ApplicationDbContext iti = new ApplicationDbContext();
        
        // GET: Student
        public ActionResult Index()
        {

            var viewModel = new StudentViewModel()
            {
                DepartmentList = iti.Departments.ToList(),
                StudentList = iti.Students.ToList()

            };
            //iti.Students.Select(s=>s.Department_Key);
            //(from s in iti.Students select new Student() {FirstName=s.FirstName,LastName=s.LastName,BirthDate=s.BirthDate,Email=s.Email,UserName=s.UserName,Password=s.Password,Attend_Balance=s.Attend_Balance,Telephone=s.Telephone,Address=s.Address,Department_Key=s.Department_Key }).ToList();
            /*(from s in iti.Students
                      join d in iti.Departments on s.Department_Key equals d.Department_Id
                      select new Std_Dept() { dept_name = d.dept_name, name = s.name, age = s.age, id = s.id }).ToList();*/
            return View(viewModel);
        }
        //[HttpGet]
        //public ActionResult Create_Student()
        //{
        //    ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
        //    return View();
        //}
        [HttpPost]
        public ActionResult Create_Student(ViewModel.StudentViewModel std)
        {
            Student std2 = std.Student;
            //int? selected_dept= std.Department_Key;
            //string cap= iti.Departments.Where(d => d.Department_Id == selected_dept).Select(d => d.Capacity).ToString();
            //string count_std = iti.Students.Count().ToString();

            //if (ModelState.IsValid)
            //{
            iti.Students.Add(std2);
                    try
                    {
                        iti.SaveChanges();
                var models = iti.Students;
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
        [HttpGet]
        public ActionResult edit(int Id)
        {
          

            var viewModel2 = new StudentViewModel()
            {
                DepartmentList = iti.Departments.ToList(),
                StudentList = iti.Students.ToList(),
                Student = iti.Students.SingleOrDefault(a => a.Student_Id == Id)


        };
            return View(viewModel2);
        }

        [HttpPost]
        public ActionResult edit(ViewModel.StudentViewModel st)
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
        public ActionResult delete(int Id)
        {  
            Student std = iti.Students.SingleOrDefault(a => a.Student_Id == Id);
            return View(std);
        }
        [HttpPost]
        public ActionResult delete(Student std)
        {
            
            iti.Students.Remove(std);
            iti.SaveChanges();
            return RedirectToAction("index");
        }

    }
}

//         catch (System.Data.Entity.Validation.DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//            eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//}