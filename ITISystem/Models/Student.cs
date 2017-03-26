﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public class Student
    {
        [Key]
        public int Student_Id { get; set; }
        [Required]
        [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string FirstName { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Please enter valid Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Enter Valid Date")]
        [RegularExpression("")]
        public DateTime BirthDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email")]
        [EmailAddress]
        
        public string Email { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Please Enter Strong Password ")]
        [RegularExpression("[^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$]")]
        public string Password { get; set; }
        [Range(0, 600, ErrorMessage = "Please enter valid Number")]
        [Required]
        public int Attend_Balance { get; set; }
        [Range(7, 11, ErrorMessage = "Please enter valid Phone")]

        [DataType(DataType.PhoneNumber, ErrorMessage = "Enter Valid Phone")]
        
        public int Telephone { get; set; }
        
        public FullAddress Address { get; set; }
        [ForeignKey("Department")]
        public int? Department_Key { get; set; }
        public virtual Department Department { get; set; }
    }
}