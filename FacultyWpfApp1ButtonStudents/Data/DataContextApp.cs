using FacultyWpfApp1ButtonStudents.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;



namespace FacultyWpfApp1ButtonStudents.Data
{
    public class DataContextApp
    {
        public ObservableCollection<Subject> Subjects { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<SubjectStudent> SubjectStudents { get; set; }
        public ObservableCollection<SubjectStudentJoin> SubjectStudentsJoins { get; set; }

        public DataContextApp()
        {
            GenerateDataSubjects();
            GenerateDataStudents();
            GenerateSubjectStudents();
            GenerateSubjectStudentsJoins();

            Include();
        }

        private void Include()
        {
            foreach (var student in Students)
            {
                int studentId = student.Id;
                student.Subjects = new ObservableCollection<Subject>
                (
                    SubjectStudents
                    .Where(cs => cs.StudentId == studentId)
                    .Select(cs => Subjects.Single(crs => crs.Id == cs.SubjectId))
                );
            }
            foreach (var subject in Subjects)
            {
                int courceId = subject.Id;
                subject.Students = new ObservableCollection<Student>
                (
                    SubjectStudents
                    .Where(cs => cs.SubjectId == courceId)
                    .Select(cs => Students.Single(std => std.Id == cs.StudentId))
                );
            }
        }




        // ---- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        public void GenerateDataSubjects()
        {
            Subjects = new ObservableCollection<Subject>
            (
                Enumerable.Range(1, 10).Select(i => new Subject()
                {
                    Id = i,
                    Name = $"NameSubject-{i}",
                    Description = $"DescriptionSubject-{i}"
                })
            );
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
        }


        public void GenerateSubjectStudents()
        {
            Random random = new Random(12345678); // Для повторяемости результата

            // распределяем всех студентов по курсом псевдослучайным образом
            SubjectStudents = new ObservableCollection<SubjectStudent>
            (
                Students
                .SelectMany
                (
                    std => Subjects
                                 .OrderBy(crs => random.Next()) // Случайная сортировка предметов
                                 .Take(random.Next(1, 6))       // Получение первых случайных предметов
                                 .Select(sbj => (std, sbj))     // Получение сочетаний студент-предмет
                )
                .OrderBy(ss => random.Next())                   // Случайное перемешивание
                .Select((ss, ind) => new SubjectStudent()        // Получение сущностей сочетаний студент-предмет
                                    {
                                        Id = ind + 1,
                                        SubjectId = ss.sbj.Id,
                                        StudentId = ss.std.Id
                                    }
                        )
            );
        }



        public void AddSubjectStudent()
        {
            // in development
        }



        public void GenerateSubjectStudentsJoins()
        {
            SubjectStudentsJoins = new ObservableCollection<SubjectStudentJoin>(SubjectStudents.Select
            (
                cs =>
                new SubjectStudentJoin
                {
                    IdSubjectStudent = cs.Id,
                    IdSubject = cs.SubjectId,
                    IdStudent = cs.StudentId,

                    NameStudent = Students.Single(std => std.Id == cs.StudentId).Name
                })
            );
        }

        public void AddSubjectStudentView()
        {
            // in development
        }

    }
}
