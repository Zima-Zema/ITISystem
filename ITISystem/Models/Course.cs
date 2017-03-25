using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public enum CourseStatus
    {
        NotFinish,
        Finish
    }
    public class Course
    {
        public Course()
        {
            CrsDeptInstr = new List<Dept_Crs_Instr>();
            Std_Crs_Instr = new List<Models.Std_Crs_Instr>();
            Exams = new List<Exam>();
        }
        [Key]
        public int Course_Id { get; set; }
        //
        [Required]
        [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        [Index(IsUnique = true)]
        [MaxLength(20)]
        public string Name { get; set; }
        //
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public int Lec_Duration { get; set; }
        //
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public int Lab_Duration { get; set; }
        //
        [Required]
        public CourseStatus Status { get; set; }    //done by CheckBox
        //
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public int Duration { get; set; }
        public virtual List<Dept_Crs_Instr> CrsDeptInstr { set; get; }
        public virtual List<Std_Crs_Instr> Std_Crs_Instr { set; get; }
        public virtual List<Exam> Exams { set; get; }
    }
}