using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITISystem.ViewModel
{
    
    public class QuestionViewModel
    {

        public Question QuestionObj { get; set; }
        public IEnumerable<Course> CourseList { get; set; }

        public List<string> answers = new List<string>()
        {
            "A","B","C","D"
        };
    }
}