using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITISystem.Models;

namespace ITISystem.Controllers
{
    
    public class InstructorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Instructor
      

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getAll()
        {
            return View(db.Instructor);
        }
        [HttpGet]
        public ActionResult createInstructor()
        {
            
            return View();

        }


        [HttpPost]
        public ActionResult createInstructor(Instructor ins)
        {
            if (ModelState.IsValid)
            {
                db.Instructor.Add(ins);
                db.SaveChanges();
                return RedirectToAction("getAll");
            }
            return View();

        }

    }
}