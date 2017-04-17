using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public class Exam
    {
        public Exam()
        {
            Questions = new List<Question>();
            Std_Exam_Ques = new List<Models.Std_Exam_Quest>();
            Std_Exams = new List<Std_Exam>();
        }
        [Key]
        public int Exam_id { get; set; }
        //
        [Required][Range(0, 1000, ErrorMessage = "Please enter valid Number")]
        public int Duration { get; set; }
        //
        [Required] [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string Subject { get; set; }
        //
        [DataType(DataType.DateTime, ErrorMessage = "Enter Valid DateTime")]
        public DateTime from { get; set; }
        //
        [DataType(DataType.DateTime, ErrorMessage = "Enter Valid DateTime")]
        public DateTime to { get; set; }

        [Range(1,20)]
        public int? NumberOfQuestion { get; set; }

        //
        [ForeignKey("Courses")]
        public int Course_key { get; set; }
        public virtual Course Courses { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<Std_Exam_Quest> Std_Exam_Ques { get; set; }
        public virtual List<Std_Exam> Std_Exams { get; set; }
    }
}