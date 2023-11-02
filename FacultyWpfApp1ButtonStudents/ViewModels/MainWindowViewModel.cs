using FacultyWpfApp1ButtonStudents.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacultyWpfApp1ButtonStudents.ViewModels;
using FacultyWpfApp1ButtonStudents.Views;
using System.Diagnostics;
using FacultyWpfApp1ButtonStudents.Models;

namespace FacultyWpfApp1ButtonStudents.ViewModels
{
    class MainWindowViewModel : BaseVM
    {
        public DataContextApp dc;

        CoursesViewModel coursesViewModel;
        CoursesStudentsJoinViewModel coursesStudentsJoinViewModel;
        StudentsViewModel studentsViewModel;
        
        public MainWindowViewModel()
        {
            this.dc = new DataContextApp();
            
            // CoursesViewModel
            this.coursesViewModel = new CoursesViewModel(this);
            coursesViewModel.LoadDataTest();
            this.CoursesView = coursesViewModel;
            
            // CoursesStudentsJoinViewModel
            coursesStudentsJoinViewModel = new CoursesStudentsJoinViewModel(this);
            coursesStudentsJoinViewModel.LoadDataTest();
            this.CoursesStudentsJoinView = coursesStudentsJoinViewModel;

            // StudentsViewModel
            studentsViewModel = new StudentsViewModel(this.dc);
            studentsViewModel.LoadDataTest();
            this.StudentsView = studentsViewModel;
        }


        // SelectedCourse
        private Course selectedCourse;

        public Course SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                selectedCourse = value;
                Debug.WriteLine("\n\n === === === MainWindowViewModel.SelectedCourse === === ===");
                if (selectedCourse == null)
                {
                    Debug.WriteLine($"SelectedCourse = null !!!");
                    return;
                }
                Debug.WriteLine($"SelectedCourse.NameCourse -- {selectedCourse.NameCourse}");

                // Установить критерий фильтрации для `CoursesStudentsJoinViewModel`
                coursesStudentsJoinViewModel.CourseFilter = selectedCourse;

                // Установить критерий фильтрации для `StudentsViewModel`
                // var fdf = coursesStudentsJoinViewModel.
                 var cSJ = coursesStudentsJoinViewModel.GetCoursesStudentsJoin(selectedCourse);
                this.studentsViewModel.LoadDataUnion(cSJ);

                RaisePropertyChanged(nameof(SelectedCourse));
            }
        }


        /// <summary>
        /// View
        /// </summary>
        #region View === === === === === === === === ===
        // CoursesView
        private BaseVM coursesView;

        public BaseVM CoursesView
        {
            get { return coursesView; }
            set
            {
                coursesView = value;
                RaisePropertyChanged(nameof(CoursesView));
            }
        }


        // CourseStudentsView        
        private BaseVM _сoursesStudentsJoinView;

        public BaseVM CoursesStudentsJoinView
        {
            get { return _сoursesStudentsJoinView; }
            set
            {
                _сoursesStudentsJoinView = value;
                RaisePropertyChanged(nameof(CoursesStudentsJoinView));
            }
        }

        // CourseStudentsView
        private BaseVM _studentsView;

        public BaseVM StudentsView
        {
            get { return _studentsView; }
            set
            {
                _studentsView = value;
                RaisePropertyChanged(nameof(StudentsView));
            }
        }



        #endregion
    }
}
