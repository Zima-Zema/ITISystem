using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Excel;
using System.IO;
using System.Data;

namespace ITISystem.Controllers
{
    public class DepartmentController : Controller
    {
        ApplicationDbContext iti = new ApplicationDbContext();
        // GET: Department
        public ActionResult Index()
        {
            var depts = iti.Departments.Include(de=>de.instructor_mang);
            return View(depts.ToList());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var depts = iti.Departments.Include(dd => dd.instructor_mang).Where(s => s.Department_Id == id).Single();
            var ins = iti.Instructor.ToList();
            ViewBag.ins = new SelectList(ins, "Instructor_Id", "Name", depts.manger_key);
            return PartialView(depts);
        }
        [HttpPost]
        public ActionResult Edit(Department dept)
        {
            //var depts = iti.Departments.Include(dd => dd.instructor_mang).ToList();
            var depts = iti.Departments.Include(dd => dd.instructor_mang).Where(s => s.Department_Id == dept.Department_Id).Single();

            var ins = iti.Instructor.ToList();

            if (ModelState.IsValid)
            {
                var deptsold = iti.Departments.SingleOrDefault(s=>s.Department_Id==dept.Department_Id);
                deptsold.Name = dept.Name;
                deptsold.Capacity = dept.Capacity;
                deptsold.manger_key = dept.manger_key;
                iti.SaveChanges();
                var deptList = iti.Departments.Include(dd => dd.instructor_mang).ToList();

                return PartialView("Edittable", deptList);
            }
            ViewBag.ins = new SelectList(ins, "Instructor_Id", "Name", depts.manger_key);

            return PartialView(depts);


        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var depts = iti.Departments.Include(dd => dd.instructor_mang).Where(s => s.Department_Id == id).Single();
            return PartialView(depts);
        }
        [HttpPost]
        public ActionResult Delete(Department dept)
        {
            var depart = iti.Departments.Include(m=>m.Instructors).Include(m=>m.Students).SingleOrDefault(s => s.Department_Id == dept.Department_Id);
            iti.Departments.Remove(depart);
            iti.SaveChanges();
            var depts = iti.Departments.Include(dd => dd.instructor_mang).ToList();

            return PartialView("Edittable", depts);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var ins = iti.Instructor.ToList();
            //.Instructors.ToList();
            ViewBag.ins = new SelectList(ins, "Instructor_Id", "Name", 1);
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Department dept)
        {
            var deptss = iti.Departments.Include(dd => dd.instructor_mang).ToList();
            if (ModelState.IsValid)
            {
                iti.Departments.Add(dept);
                iti.SaveChanges();
               
                return PartialView("Edittable", deptss);

            }
            var ins = iti.Instructor.ToList();

            ViewBag.ins = new SelectList(ins, "Instructor_Id", "Name", 1);
            return PartialView();
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var depts = iti.Departments.Include(dd => dd.instructor_mang).Where(s => s.Department_Id == id).Single();
            return PartialView(depts);
        }
        public ActionResult Studentspage()
      {
            ViewBag.dept = iti.Departments.ToList();
            return View();
        }
        public ActionResult Fillstudent(int state)
        {
            var students = iti.Students.Where(c => c.Department_Key == state).Select(m=>new { name = m.FirstName + " " + m.LastName }).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Coursespage()
        {
            ViewBag.dept = iti.Departments.ToList();
            return View();
        }
        public ActionResult Fillcourse(int state)
        {
            // var students = iti.Students.Where(c => c.Department_Key == state).Select(m => new { name = m.FirstName + " " + m.LastName }).ToList();
            var courses = iti.Departments.SingleOrDefault(m => m.Department_Id == state).Courses.Select(m => new { name = m.Name }).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Instructorspage()
        {
            ViewBag.dept = iti.Departments.ToList();
            return View();
        }
        public ActionResult Fillinstructor(string Dept)
        {
            int ID = int.Parse(Dept);
            List<Instructor>instructors = iti.Departments.SingleOrDefault(m => m.Department_Id == ID).Instructors.ToList();
            ViewBag.manager = iti.Departments.SingleOrDefault(m => m.Department_Id == ID).manger_key;
            return PartialView(instructors);
    
        }
        public ActionResult Exelpage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Exelpage(HttpPostedFileBase uploadfile)
        {
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    //ExcelDataReader works on binary excel file
                    Stream stream = uploadfile.InputStream;
                    //We need to written the Interface.
                    IExcelDataReader reader = null;
                    if (uploadfile.FileName.EndsWith(".xls"))
                    {
                        //reads the excel file with .xls extension
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (uploadfile.FileName.EndsWith(".xlsx"))
                    {
                        //reads excel file with .xlsx extension
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        //Shows error if uploaded file is not Excel file
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                    //treats the first row of excel file as Coluymn Names
                    reader.IsFirstRowAsColumnNames = true;
                    //Adding reader data to DataSet()
                    DataSet result = reader.AsDataSet();
                    reader.Close();
                   
                    if(result.Tables.Count>0)
                    {
                        if (result.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                            {
                                Department dept = new Department();
                                var s = result.Tables[0].Rows[i].ItemArray;
                                dept.Name = s[0].ToString();
                                dept.Capacity = int.Parse(s[1].ToString());
                                dept.manger_key = int.Parse(s[2].ToString());
                                iti.Departments.Add(dept);
                            }
                            iti.SaveChanges();
                        }
                    }
                    //Sending result data to View
                    return View(result.Tables[0]);
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please upload your file");
            }
            return View();
        }
        public ActionResult Managerpage()
        {
            ViewBag.dept = iti.Departments.ToList();
            return View();
        }
        public ActionResult selectinstructor( string Dept)
        {
            int ID = int.Parse(Dept);
            List<Instructor> instructors = iti.Departments.SingleOrDefault(m => m.Department_Id == ID).Instructors.ToList();
            ViewBag.manager = iti.Departments.SingleOrDefault(m => m.Department_Id == ID).manger_key;
            ViewBag.dept = iti.Departments.SingleOrDefault(m => m.Department_Id == ID);
            return PartialView(instructors);
        }
        public ActionResult changes(int state)
        {
            var id = iti.Instructor.SingleOrDefault(m => m.Instructor_Id == state).Department_Key;
            Department dept = iti.Departments.SingleOrDefault(m => m.Department_Id == id);

            dept.manger_key = state;
            iti.SaveChanges();
            string s = "success";
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }
}