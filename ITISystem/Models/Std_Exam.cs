using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public class Std_Exam
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Students")]
        public int? Student_key { get; set; }
        //
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Exams")]
        public int? Exam_key { get; set; }
        //
        [Range(0, 600, ErrorMessage = "Please enter valid Number")]
        public int Exam_grade { get; set; }
        //
        public virtual Student Students { get; set; }
        public virtual Exam Exams { get; set; }
    }
}