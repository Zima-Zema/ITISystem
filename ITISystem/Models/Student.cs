using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITISystem.Models
{
    public class Student
    {
        public Student()
        {
            Std_Crs_Instr = new List<Models.Std_Crs_Instr>();
            Std_Exam_Ques = new List<Models.Std_Exam_Quest>();
            Std_Exams = new List<Std_Exam>();
        }
        [Key]
        public int Student_Id { get; set; }
        [Required]
        [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string FirstName { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string LastName { get; set; }
        //[DataType(DataType.Date, ErrorMessage = "Enter Valid Date")]
        //[RegularExpression("dd/mm/yyyy")]
        // public DateTime BirthDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email")]
        [EmailAddress]    
        public string Email { get; set; }
        //
        [Required]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        //
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Please Enter Strong Password ")]
      //[RegularExpression("[^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$]")]
        public string Password { get; set; }
        //
        [Range(0, 600, ErrorMessage = "Please enter valid Number")]
        [Required]
        public int Attend_Balance { get; set; }
        //   
      //   [DataType(DataType.PhoneNumber)]  //212-666-1234
                                                                                        //[RegularExpression(@"^[0-9]\d{2}-\d{3}-\d{4}$", ErrorMessage = "Not a valid Phone number")]
        [Phone]
        public string Telephone { get; set; }
        //
        public FullAddress Address { get; set; }
        //
        [ForeignKey("Department")]
        public int? Department_Key { get; set; }
        //
        public virtual Department Department { get; set; }
        //
        public virtual Attendance Attendances { get; set; }
        //
        public virtual List<Std_Crs_Instr> Std_Crs_Instr { get; set; }
        public virtual List<Std_Exam_Quest> Std_Exam_Ques { get; set; }
        public virtual List<Std_Exam> Std_Exams { get; set; }
    }
}