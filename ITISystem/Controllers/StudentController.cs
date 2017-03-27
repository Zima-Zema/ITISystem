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
            var models = iti.Students;
            //iti.Students.Select(s=>s.Department_Key);
            //(from s in iti.Students select new Student() {FirstName=s.FirstName,LastName=s.LastName,BirthDate=s.BirthDate,Email=s.Email,UserName=s.UserName,Password=s.Password,Attend_Balance=s.Attend_Balance,Telephone=s.Telephone,Address=s.Address,Department_Key=s.Department_Key }).ToList();
            /*(from s in iti.Students
                      join d in iti.Departments on s.Department_Key equals d.Department_Id
                      select new Std_Dept() { dept_name = d.dept_name, name = s.name, age = s.age, id = s.id }).ToList();*/
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
           //int? selected_dept= std.Department_Key;
           //string cap= iti.Departments.Where(d => d.Department_Id == selected_dept).Select(d => d.Capacity).ToString();
           //string count_std = iti.Students.Count().ToString();

            //if (ModelState.IsValid)
            //{
                iti.Students.Add(std);
                    try
                    {
                        iti.SaveChanges();
                        return RedirectToAction("index");
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