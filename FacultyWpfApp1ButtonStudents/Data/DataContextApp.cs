using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacultyWpfApp1ButtonStudents.Models;



namespace FacultyWpfApp1ButtonStudents.Data
{
    public class DataContextApp
    {
        public DataContextApp()
        {
            GenerateDataCourses();
            GenerateDataStudents();
            GenerateCourseStudent();
            GenerateCourseStudentsJoin();
        }


        private ObservableCollection<Course> _courses;

        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set { _courses = value; }
        }


        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set { _students = value; }
        }


        private ObservableCollection<CourseStudent> _courseStudents;

        public ObservableCollection<CourseStudent> CourseStudents
        {
            get { return _courseStudents; }
            set { _courseStudents = value; }
        }


        private ObservableCollection<CourseStudentJoin> _courseStudentJoin;

        public ObservableCollection<CourseStudentJoin> CoursesStudentsJoins
        {
            get { return _courseStudentJoin; }
            set { _courseStudentJoin = value; }
        }


        // ---- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        public void GenerateDataCourses()
        {
            Courses = new ObservableCollection<Course>();
            for (int i = 1; i < 11; i++)
            {
                var сourse = new Course()
                {
                    IdCourse = i,
                    NameCourse = $"NameCourse-{i}",
                    Description = $"DescriptionCourses-{i}"
                };
                Courses.Add(сourse);
            }
        }


        public void GenerateDataStudents()
        {
            Students = new ObservableCollection<Student>();
            for (int i = 1; i < 101; i++)
            {
                var provider = new Student()
                {
                    IdStudent = i,
                    NameStudent = $"NameStudent-{i}",
                    Description = $"DescriptionStudent-{i}"
                };
                Students.Add(provider);
            }
        }


        public void GenerateCourseStudent()
        {
            CourseStudents = new ObservableCollection<CourseStudent>();

            int idCourseStudent      = 1;
            int courseCurrentStudent = 1;
            

            for (int iC = 3; iC < 11; iC++)                                 // Course
            {
                int step = 10;
                int courseFirstStudent = courseCurrentStudent;
                int courseLastStudent = courseFirstStudent + step;

                for (int iS = courseFirstStudent; iS < courseLastStudent; iS++) // Student
                {
                    // idIndexProvider         
                    var indexProveder = new CourseStudent()
                    {
                        IdCourseStudent = idCourseStudent,
                        IdCourse         = iC,
                        IdStudent        = iS
                    };
                    CourseStudents.Add(indexProveder);
                    idCourseStudent++;
                }
                courseCurrentStudent = courseLastStudent++;
            }          
           
        }

        public void AddCourseStudent()
        {
            // in development
        }



        public void GenerateCourseStudentsJoin()
        {
            var CourseStudentsJoin = CourseStudents.Join(Students,
                 cS => cS.IdStudent,
                 s => s.IdStudent,
                (cS, s) => new CourseStudentJoin
                {
                    IdCourseStudent = cS.IdCourseStudent,
                    IdCourse = cS.IdCourse,
                    IdStudent = cS.IdStudent,

                    NameStudent = s.NameStudent
                }).ToList();

            CoursesStudentsJoins = new ObservableCollection<CourseStudentJoin>(CourseStudentsJoin);
        }

        public void AddCourseStudentView()
        {
            // in development
        }
        
    }
}
