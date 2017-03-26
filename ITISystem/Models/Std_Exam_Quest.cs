using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public class Std_Exam_Quest
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
        [Key]
        [Column(Order = 3)]
        [ForeignKey("Questions")]
        public int? Question_key { get; set; }
        //
        [DataType(DataType.Text, ErrorMessage = "Not valid Name")]
        public string Answer { get; set; }
        //
        public virtual Student Students { get; set; }
        public virtual Exam Exams { get; set; }
        public virtual Question Questions { get; set; }
    }
}