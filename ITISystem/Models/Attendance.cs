using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public class Attendance
    {
        [Key]
       // [Column("Student_Id", Order = 1)]
        [ForeignKey("Student")]   
        public int Student_key { get; set; }
     //   [Key]
     //   [Column(Order = 2)]
        [DataType(DataType.Date, ErrorMessage = "Enter Valid Date")]
        public DateTime Date { get; set; }
        //
        [DataType(DataType.Date, ErrorMessage = "Enter Valid Date")]
        public DateTime Arrive_time { get; set; }
        //
        [DataType(DataType.Date, ErrorMessage = "Enter Valid Date")]
        public DateTime Leave_time { get; set; }   
        //
        [Required]
        public virtual Student Student { get; set; }
    }
}