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
        //
        [ForeignKey("Courses")]
        public int Course_key { get; set; }
        public virtual Course Courses { get; set; }
    }
}