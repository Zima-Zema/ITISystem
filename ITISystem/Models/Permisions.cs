using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
     public enum premission {
        reject,allow
    }
    public class Permisions
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Students")]
        public int Student_key { get; set; }
        //
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Instructors")]
        public int Instructor_key { get; set; }
        //
        //[DataType(DataType.Date, ErrorMessage = "Enter Valid Date")]
        //[RegularExpression("dd/mm/yyyy")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        //
        public premission Type { get; set; }

        public virtual Instructor Instructors { get; set; }

        public virtual Student Students { get; set; }


    }
}