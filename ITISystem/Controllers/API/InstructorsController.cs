using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ITISystem.Models;
using System.Web.Mvc;

namespace ITISystem.Controllers.API
{
    public class InstructorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Instructors
        //public IHttpActionResult GetInstructor()
        //{
        //    return Ok(db.Instructor.ToList());
        //}


        // GET: api/Instructors/5
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult GetInstructor(int id)
        {
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return Ok(instructor);
        }

        // PUT: api/Instructors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInstructor(int id, Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instructor.Instructor_Id)
            {
                return BadRequest();
            }

            db.Entry(instructor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Instructors
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult PostInstructor(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Instructor.Add(instructor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = instructor.Instructor_Id }, instructor);
        }

        // DELETE: api/Instructors/5
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult DeleteInstructor(int id)
        {
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            IEnumerable<Department> departments = db.Departments.Where(dept => dept.manger_key == id);
            if (departments.Count()>0)
            {
                foreach (var item in departments)
                {
                    item.manger_key = null;
                }
            }
            IEnumerable<Dept_Crs_Instr> list = instructor.InstrDeptCrs.FindAll(dd => dd.Instructor_key == id);
            if (list.Count()>0)
            {
                db.DeptS_CrS_InstrS.RemoveRange(list);

            }
            IEnumerable<Std_Crs_Instr> list2 = instructor.Std_Crs_Instr.FindAll(ss => ss.Instructor_key == id);
            if (list2.Count()>0)
            {
                db.StdS_CrS_InstrS.RemoveRange(list2);
            }
            instructor.Courses.Clear();
            db.Instructor.Remove(instructor);
            db.SaveChanges();

            return Ok(instructor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorExists(int id)
        {
            return db.Instructor.Count(e => e.Instructor_Id == id) > 0;
        }
    }
}