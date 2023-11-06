using FacultyWpfApp1ButtonStudents.Data;
using FacultyWpfApp1ButtonStudents.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FacultyWpfApp1ButtonStudents.ViewModels
{
    class StudentsViewModel : BaseVM
    {
        // MainWindowViewModel mainWindowViewModel;

        DataContextApp dc;
        // ctor
        public StudentsViewModel(DataContextApp dc)
        {
            //  this.mainWindowViewModel = mainWindowViewModel;

            this.dc = dc;
        }


        // Courses
        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;



                RaisePropertyChanged(nameof(Students));
            }
        }


        private ObservableCollection<Student> _studentViews;

        public ObservableCollection<Student> StudentsView
        {
            get { return _studentViews; }
            set
            {
                _studentViews = value;



                RaisePropertyChanged(nameof(StudentsView));
            }
        }



        // SelectedCourse
        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                Debug.WriteLine("\n\n === === === CoursesViewModel.SelectedCourse === === ===");
                if (SelectedStudent == null)
                {
                    Debug.WriteLine($"SelectedCourse = null !!!");
                    return;
                }
                Debug.WriteLine($"SelectedCourse.NameCourse -- {SelectedStudent.NameStudent}");
                // this.mainWindowViewModel.SelectedCourse = SelectedStudent;
                RaisePropertyChanged(nameof(SelectedStudent));
            }
        }


        public void LoadDataTest()
        {
            // СalculationIndexs = this.DataContextApp.СalculationIndexes;
            Students = dc.Students;
            // return dc.Courses;

            Debug.WriteLine($"\n\n === === === CoursesViewModel === === === ");
            Debug.WriteLine($"LoadDataTest()");

        }

        public void LoadDataUnion(ObservableCollection<CourseStudentJoin> courseStudentJoin)
        {

            // var result = Students.Where(cSJ => !courseStudentJoin
            //                     .Select(s => s.IdStudent).Contains(cSJ.IdStudent));

            // Solut-2
            var result = Students.Where(cSJ => !courseStudentJoin.Select(s => s.IdStudent).Contains(cSJ.IdStudent));
            StudentsView = new ObservableCollection<Student>(result);

            // Solut-1
            // StudentsView = new ObservableCollection<Student>(Students
            //                    .Where(cSJ => !courseStudentJoin.Select(s => s.IdStudent).Contains(cSJ.IdStudent)));

            Debug.WriteLine($"\n\n=== === === ProvidersViewModel === === ===");
            Debug.WriteLine($"LoadDataUnion(...)  ");
            // Debug.WriteLine($"ProvidersView.Count -- {ProvidersView.Count}");
            // Debug.WriteLine($"Providers.Count -- {Providers.Count}");

            //foreach (var item in ProvidersView)
            //{
            //    Debug.WriteLine($"item.Id: {item.Id} | item.Name: {item.Name}");
            //}
        }
    }
}
