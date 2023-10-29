using FacultyWpfApp1ButtonStudents.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;



namespace FacultyWpfApp1ButtonStudents.Data
{
    public class DataContextApp
    {
        //private ObservableCollection<Course> _courses;

        public ObservableCollection<Course> Courses { get; set; }
        //{
        //    get { return _courses; }
        //    set { _courses = value; }
        //}


        //private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students { get; set; }
        //{
        //    get { return _students; }
        //    set { _students = value; }
        //}


        //private ObservableCollection<CourseStudent> _courseStudents;

        public ObservableCollection<CourseStudent> CourseStudents { get; set; }
        //{
        //    get { return _courseStudents; }
        //    set { _courseStudents = value; }
        //}

        //private ObservableCollection<CourseStudentJoin> _courseStudentJoin;

        public ObservableCollection<CourseStudentJoin> CoursesStudentsJoins { get; set; }
        //{
        //    get { return _courseStudentJoin; }
        //    set { _courseStudentJoin = value; }
        //}
        public DataContextApp()
        {
            GenerateDataCourses();
            GenerateDataStudents();
            GenerateCourseStudent();
            GenerateCourseStudentsJoin();

            Include();
        }

        private void Include()
        {
            foreach (var student in Students)
            {
                int studentId = student.Id;
                student.Courses = new ObservableCollection<Course>
                (
                    CourseStudents
                    .Where(cs => cs.StudentId == studentId)
                    .Select(cs => Courses.Single(crs => crs.Id == cs.CourseId))
                );
            }
            foreach (var course in Courses)
            {
                int courceId = course.Id;
                course.Students = new ObservableCollection<Student>
                (
                    CourseStudents
                    .Where(cs => cs.CourseId == courceId)
                    .Select(cs => Students.Single(std => std.Id == cs.StudentId))
                );
            }
        }




        // ---- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        public void GenerateDataCourses()
        {
            Courses = new ObservableCollection<Course>
            (
                Enumerable.Range(1, 10).Select(i => new Course()
                {
                    Id = i,
                    Name = $"NameCourse-{i}",
                    Description = $"DescriptionCourses-{i}"
                })
            );
            //for (int i = 1; i < 11; i++)
            //{
            //    var сourse = new Course()
            //    {
            //        IdCourse = i,
            //        NameCourse = $"NameCourse-{i}",
            //        Description = $"DescriptionCourses-{i}"
            //    };
            //    Courses.Add(сourse);
            //}
        }


        public void GenerateDataStudents()
        {
            Students = new ObservableCollection<Student>
            (
                Enumerable.Range(1, 100).Select(i => new Student()
                {
                    Id = i,
                    Name = $"NameStudent-{i}",
                    Description = $"DescriptionStudent-{i}"
                }
            ));
            //for (int i = 1; i < 101; i++)
            //{
            //    var provider = new Student()
            //    {
            //        IdStudent = i,
            //        NameStudent = $"NameStudent-{i}",
            //        Description = $"DescriptionStudent-{i}"
            //    };
            //    Students.Add(provider);
            //}
        }


        public void GenerateCourseStudent()
        {
            Random random = new Random(12345678); // Для повторяемости результата

            // распределяем всех студентов по курсом псевдослучайным образом
            CourseStudents = new ObservableCollection<CourseStudent>
            (
                Students
                .SelectMany
                (
                    std => Courses
                                 .OrderBy(crs => random.Next()) // Случайная сортировка предметов
                                 .Take(random.Next(1, 6))       // Получение первых случайных предметов
                                 .Select(sbj => (std, sbj))     // Получение сочетаний студент-предмет
                )
                .OrderBy(ss => random.Next())                   // Случайное перемешивание
                .Select((ss, ind) => new CourseStudent()        // Получение сущностей сочетаний студент-предмет
                                    {
                                        IdCourseStudent = ind + 1,
                                        CourseId = ss.sbj.Id,
                                        StudentId = ss.std.Id
                                    }
                        )
            );

            //int idCourseStudent = 1;
            //int courseCurrentStudent = 1;


            //for (int iC = 3; iC < 11; iC++)                                 // Course
            //{
            //    int step = 10;
            //    int courseFirstStudent = courseCurrentStudent;
            //    int courseLastStudent = courseFirstStudent + step;

            //    for (int iS = courseFirstStudent; iS < courseLastStudent; iS++) // Student
            //    {
            //        // idIndexProvider         
            //        var indexProveder = new CourseStudent()
            //        {
            //            IdCourseStudent = idCourseStudent,
            //            CourseId = iC,
            //            StudentId = iS
            //        };
            //        CourseStudents.Add(indexProveder);
            //        idCourseStudent++;
            //    }
            //    courseCurrentStudent = courseLastStudent++;
            //}

        }



        public void AddCourseStudent()
        {
            // in development
        }



        public void GenerateCourseStudentsJoin()
        {
            //var CourseStudentsJoin = CourseStudents.Join(Students,
            //     cS => cS.StudentId,
            //     s => s.Id,
            //    (cS, s) => new CourseStudentJoin
            //    {
            //        IdCourseStudent = cS.IdCourseStudent,
            //        IdCourse = cS.CourseId,
            //        IdStudent = cS.StudentId,

            //        NameStudent = s.Name
            //    }).ToList();

            CoursesStudentsJoins = new ObservableCollection<CourseStudentJoin>(CourseStudents.Select
            (
                cs =>
                new CourseStudentJoin
                {
                    IdCourseStudent = cs.IdCourseStudent,
                    IdCourse = cs.CourseId,
                    IdStudent = cs.StudentId,

                    NameStudent = Students.Single(std => std.Id == cs.StudentId).Name
                })
            );
        }

        public void AddCourseStudentView()
        {
            // in development
        }

    }
}
