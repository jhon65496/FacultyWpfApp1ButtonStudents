using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyWpfApp1ButtonStudents.Models
{
    public class CourseStudentJoin  
    {   
        public int IdCourseStudent { get; set; }
        public int IdCourse { get; set; }
        public int IdStudent { get; set; }
        public string NameStudent { get; set; }
    }
}
