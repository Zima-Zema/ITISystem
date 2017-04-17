using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public enum Work_Status
    {
        internal_stat,
        external_stat
    }
    public class Instructor
    {
        public Instructor()
        {
            InstrDeptCrs = new List<Dept_Crs_Instr>();
            Std_Crs_Instr = new List<Models.Std_Crs_Instr>();
            Courses = new List<Course>();
        }
        [Key]
        public int Instructor_Id { get; set; }
        //
        [Required][Display(Name ="Full Name")]
        [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string Name { get; set; }
        //
        [DataType(DataType.Date, ErrorMessage = "Enter Valid Date")][Display(Name ="Birth Date")]
        public DateTime BirthDate { get; set; }
        //
        [Required][Display(Name ="Degree")]
        [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string Degree { get; set; }
        //
        [Required]
        [Range(1955, 2016)][Display(Name = "Graduation Year")]

        public int Graduation_Year { get; set; }
        //
        [Required][Display(Name ="Work Status")]
        [Range(0, 1, ErrorMessage = "Please enter valid Number")]
        public Work_Status Work_Status { get; set; }
        [ForeignKey("Department")][Display(Name ="Department")]
        public int? Department_Key { get; set; }

        public virtual Department Department { get; set; }

        public virtual List<Dept_Crs_Instr> InstrDeptCrs { set; get; }
        public virtual List<Std_Crs_Instr> Std_Crs_Instr { set; get; }

        public virtual List<Course> Courses { get; set; }

    }
}