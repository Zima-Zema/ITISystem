using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public class Std_Crs_Instr
    {
        [Key][Column(Order = 1)][ForeignKey("Students")]
        public int? Student_key { get; set; }
        //
        [Key][Column(Order = 2)][ForeignKey("Courses")]
        public int? Course_key { get; set; }
        //
        [Key][Column(Order = 3)][ForeignKey("Instructors")]
        public int? Instructor_key { get; set; }
        public int Instr_evaluation { get; set; }
        public int Crs_evaluation { get; set; }
        public int Labs_Grade { get; set; }

        public virtual Student Students { get; set; }
        public virtual Course Courses { get; set; }
        public virtual Instructor Instructors { get; set; }
    }
}