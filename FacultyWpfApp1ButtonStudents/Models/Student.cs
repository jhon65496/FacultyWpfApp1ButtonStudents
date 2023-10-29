using System.Collections.Generic;

namespace FacultyWpfApp1ButtonStudents.Models
{
    public class Student
    {
        public int Id { get; set; }

        public int Sort { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }
        
        public string Description { get; set; }

        // Навигационное свойство
        public IList<Subject> Subjects { get; set; }
        
    }
}
