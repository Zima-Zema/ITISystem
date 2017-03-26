using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public class Department
    {
        public Department()
        {
            Students = new List<Student>();
            Instructors = new List<Instructor>();
            DeptCrsInstr = new List<Dept_Crs_Instr>();
        }
        [Key]
        public int Department_Id { get; set; }
        [Required][DataType(DataType.Text,ErrorMessage = "Please enter valid Name")]
        [Index(IsUnique =true)][MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public int Capacity { get; set; }
        public virtual List<Student> Students { get; set; }
        [ForeignKey("instructor_mang")]
        public int? manger_key { get; set; }
        public virtual Instructor instructor_mang { get; set; }
        public virtual List<Instructor> Instructors { get; set; }

        public virtual List<Dept_Crs_Instr> DeptCrsInstr { set; get; }


        // [ForeignKey("Instructors")]
        // public int? instr_key { get; set; }
        /*
         //[ForeignKey("Instructor")]
       // public int manger_key { get; set; }
        //[InverseProperty("Department_mang")]
            [InverseProperty("Department_Mang")]
           // 1 - to - many
     //   [InverseProperty("Department_Key")]
           //
             //1 - to - 1
     //   [InverseProperty("Department_Mang")]
         */
    }
}