using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View(iti.Instructor);
        }
        [HttpGet]
        public ActionResult createInstructor()
        {

            return View();

        }
        [HttpPost]
        public ActionResult createInstructor(Instructor ins)
        {
           
                //if (ModelState.IsValid)
                //{
                    iti.Instructor.Add(ins);
                    iti.SaveChanges();
                    return RedirectToAction("getAll");
            //    }
            //    else
            //    {
            //        return View(iti.Instructor);
            //    }
            //}
            //catch  { }
           
        }

        public ActionResult Edit(int id)
        {

            return Content(id.ToString());

        }



    }
}