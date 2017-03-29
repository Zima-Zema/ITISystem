using ITISystem.Models;
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
            ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
            var models = iti.Students;
            //iti.Students.Select(s=>s.Department_Key);
            //(from s in iti.Students select new Student() {FirstName=s.FirstName,LastName=s.LastName,BirthDate=s.BirthDate,Email=s.Email,UserName=s.UserName,Password=s.Password,Attend_Balance=s.Attend_Balance,Telephone=s.Telephone,Address=s.Address,Department_Key=s.Department_Key }).ToList();
            /*(from s in iti.Students
                      join d in iti.Departments on s.Department_Key equals d.Department_Id
                      select new Std_Dept() { dept_name = d.dept_name, name = s.name, age = s.age, id = s.id }).ToList();*/
            return View(models);
        }
        //[HttpGet]
        //public ActionResult Create_Student()
        //{
        //    ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
        //    return View();
        //}
        //[HttpPost]
        public ActionResult Create_Student(Student std)
        {
           
            //int? selected_dept= std.Department_Key;
            //string cap= iti.Departments.Where(d => d.Department_Id == selected_dept).Select(d => d.Capacity).ToString();
            //string count_std = iti.Students.Count().ToString();

            //if (ModelState.IsValid)
            //{
            iti.Students.Add(std);
                    try
                    {
                        iti.SaveChanges();
                var models = iti.Students;
                return PartialView(models);
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
         //   }
            //else
            //{
            //    ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
            //    return View(std);
            //}

        }
        [HttpGet]
        public ActionResult edit(int Id)
        {
            Student std = iti.Students.SingleOrDefault(a=>a.Student_Id==Id);
            ViewBag.dpts = new SelectList(iti.Departments, "Department_id", "Name");
            return PartialView(std);
        }

        [HttpPost]
        public ActionResult edit(Student st)
        {
            
            Student std_old = iti.Students.SingleOrDefault(a => a.Student_Id == st.Student_Id);
            std_old.FirstName = st.FirstName;
            std_old.LastName = st.LastName;
            std_old.Password = st.Password;
            std_old.BirthDate = st.BirthDate;
            std_old.Email = st.Email;
            std_old.UserName = st.UserName;
            std_old.Attend_Balance = st.Attend_Balance;
            std_old.Telephone = st.Telephone;
            std_old.Address.City = st.Address.City;
            std_old.Address.Country = st.Address.Country;
            std_old.Address.Street = st.Address.Street;
            std_old.Department_Key = st.Department_Key;
            iti.SaveChanges();
            var models = iti.Students;
            return PartialView("Create_Student",models);
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