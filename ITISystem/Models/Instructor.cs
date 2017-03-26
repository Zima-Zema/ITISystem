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

        [ForeignKey("Department")]
        public int? Department_Key { get; set; }
        public virtual Department Department { get; set; }
      

    }
}