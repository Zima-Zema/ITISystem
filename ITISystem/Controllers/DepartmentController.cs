using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITISystem.Controllers
{
    public class DepartmentController : Controller
    {
        ApplicationDbContext iti = new ApplicationDbContext();
        // GET: Department
        public ActionResult Index()
        {
            var depts = iti.Departments;
            return View(depts);
        }
    }
}