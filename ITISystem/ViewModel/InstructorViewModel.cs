using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITISystem.ViewModel
{
    public class InstructorViewModel
    {
        public virtual Student Students { get; set; }
        public virtual Course Courses { get; set; }
        public virtual Instructor Instructors { get; set; }

        public virtual List<Student> Studentss { get; set; }

        public virtual Std_Crs_Instr items { get; set; }

    }
}