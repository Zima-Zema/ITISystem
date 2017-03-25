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
        }
        [Key]
        public int Instructor_Id { get; set; }
        //
        [Required]
        [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string Name { get; set; }
        //
        [DataType(DataType.Date, ErrorMessage = "Enter Valid Date")]
        public DateTime BirthDate { get; set; }
        //
        [Required]
        [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string Degree { get; set; }
        //
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public int Graduation_Year { get; set; }
        //
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public Work_Status Work_Status { get; set; }
        [ForeignKey("Departments")]
        public int Department_Key { get; set; }
        public virtual Department Departments { get; set; }

        public virtual List<Dept_Crs_Instr> InstrDeptCrs { set; get; }
        public virtual List<Std_Crs_Instr> Std_Crs_Instr { set; get; }

        //  public int Department_Mang { get; set; }
        // [InverseProperty("manger_key")]
        //**************************************************//
        //
        // public int Department_Mang { get; set; }
        //
        /* public int Department_Mang { get; set; }
         [ForeignKey("Department_Key")]
         public virtual Department Department { get; set; }
         [ForeignKey("Department_Mang")]
         public virtual Department Department_mang { get; set; }*/
    }
}