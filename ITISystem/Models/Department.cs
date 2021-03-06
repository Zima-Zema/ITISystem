﻿using System;
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
        public int? Department_Id { get; set; }
        [Required][DataType(DataType.Text,ErrorMessage = "Please enter valid Name")]
        [Index(IsUnique =true)][MaxLength(50)]
        [UniqueDetartmentName]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public int Capacity { get; set; }
        public virtual List<Student> Students { get; set; }
        [ForeignKey("instructor_mang")]
        public int? manger_key { get; set; }
        public virtual Instructor instructor_mang { get; set; }
        [InverseProperty("Department")]
        public virtual List<Instructor> Instructors { get; set; }

        public virtual List<Dept_Crs_Instr> DeptCrsInstr { set; get; }

        public virtual List<Course> Courses { get; set; }
        
    }
}