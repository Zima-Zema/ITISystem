using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public class Question
    {
        public Question()
        {
            Exams = new List<Exam>();
        }
        [Key]
        public int Question_id { get; set; }
        [Required][DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string Header { get; set; }
        //
        [Required][DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string Body { get; set; }
        //
        [Required][DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public char Answer_A { get; set; }
        //
        [Required][DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public char Answer_B { get; set; }
        //
        [Required][DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public char Answer_C { get; set; }
        //
        [Required][DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public char Answer_D { get; set; }
        //
        [Required][DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public char Answer_Model { get; set; }
        //
        [Required][Range(0, 50, ErrorMessage = "Please enter valid Number")]
        public double Grade { get; set; }
        //
        [ForeignKey("Courses")]
        public int Course_key { get; set; }
        public virtual Course Courses { get; set; }
        public virtual List<Exam> Exams { get; set; }
    }
}