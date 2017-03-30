using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITISystem.ViewModel
{
    public class StudentViewModel
    {
        public Student  Student { get; set; }
        public IEnumerable<Student> StudentList { get; set; }
        public IEnumerable<Department> DepartmentList { get; set; }
    }
}