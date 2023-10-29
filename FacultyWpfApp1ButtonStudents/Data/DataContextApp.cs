using FacultyWpfApp1ButtonStudents.Common;
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
            Subjects = GenerateDataSubjects();
            Students = GenerateDataStudents();
            SubjectStudents = GenerateSubjectStudents();
            SubjectStudentsJoins = GenerateSubjectStudentsJoins();

            Include();
        }

        private void Include()
        {
            Students.ForEach(Include);
            Subjects.ForEach(Include);
        }

        public void Include(Student student)
        {
            int studentId = student.Id;
            student.Subjects = new ObservableCollection<Subject>
            (
                SubjectStudents
                .Where(cs => cs.StudentId == studentId)
                .Select(cs => Subjects.Single(crs => crs.Id == cs.SubjectId))
            );
        }

        public Student ReCreate(Student student)
        {
            int studentIndex = Students.IndexOf(student);
            var @new = new Student()
            {
                Id = student.Id,
                Name = student.Name,
                Alias = student.Alias,
                Description = student.Description,
                Sort = student.Sort
            };
            Include(student);
            if (studentIndex >= 0)
                Students[studentIndex] = @new;
            @new.Subjects.Select(ReCreate);
            return @new;
        }

        public Subject ReCreate(Subject subject)
        {
            int subjectIndex = Subjects.IndexOf(subject);
            var @new = new Subject()
            {
                Id = subject.Id,
                Name = subject.Name,
                Alias = subject.Alias,
                Description = subject.Description,
                Sort = subject.Sort
            };
            Include(subject);
            if (subjectIndex >= 0)
                Subjects[subjectIndex] = @new;
            @new.Students.Select(ReCreate);
            return @new;
        }

        public void Include(Subject subject)
        {
            int subjectId = subject.Id;
            subject.Students = new ObservableCollection<Student>
            (
                SubjectStudents
                .Where(cs => cs.SubjectId == subjectId)
                .Select(cs => Students.Single(std => std.Id == cs.StudentId))
            );
        }

        // ---- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        public static ObservableCollection<Subject> GenerateDataSubjects()
        {
            return new ObservableCollection<Subject>
            (
                Enumerable.Range(1, 10).Select(i => new Subject()
                {
                    Id = i,
                    Name = $"NameSubject-{i}",
                    Description = $"DescriptionSubject-{i}"
                })
            );
        }


        public static ObservableCollection<Student> GenerateDataStudents()
        {
            return new ObservableCollection<Student>
            (
                Enumerable.Range(1, 100).Select(i => new Student()
                {
                    Id = i,
                    Name = $"NameStudent-{i}",
                    Description = $"DescriptionStudent-{i}"
                }
            ));
        }


        public ObservableCollection<SubjectStudent> GenerateSubjectStudents()
        {
            Random random = new(12345678); // Для повторяемости результата

            // распределяем всех студентов по курсом псевдослучайным образом
            return new ObservableCollection<SubjectStudent>
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



        public ObservableCollection<SubjectStudentJoin> GenerateSubjectStudentsJoins()
        {
            return new ObservableCollection<SubjectStudentJoin>(SubjectStudents.Select
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
