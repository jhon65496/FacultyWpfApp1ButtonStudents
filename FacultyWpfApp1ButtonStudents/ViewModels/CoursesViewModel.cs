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
    class CoursesViewModel : BaseVM
    {
        MainWindowViewModel mainWindowViewModel;

        DataContextApp dc;
        // ctor
        public CoursesViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            this.dc = this.mainWindowViewModel.dc;
        }


        // Courses
        private ObservableCollection<Course> _courses;

        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set 
            { 
                _courses = value;                

                RaisePropertyChanged(nameof(Courses));
            }
        }


        // SelectedCourse
        private Course _selectedCourse;

        public Course SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                _selectedCourse = value;
                Debug.WriteLine("\n\n === === === CoursesViewModel.SelectedCourse === === ===");                
                if (SelectedCourse == null)
                {
                    Debug.WriteLine($"SelectedCourse = null !!!");
                    return;
                }
                Debug.WriteLine($"SelectedCourse.NameCourse -- {SelectedCourse.NameCourse}");
                this.mainWindowViewModel.SelectedCourse = SelectedCourse;
                RaisePropertyChanged(nameof(SelectedCourse));
            }
        }


        public void LoadDataTest()
        {
            // СalculationIndexs = this.DataContextApp.СalculationIndexes;
            Courses = dc.Courses;
            // return dc.Courses;

            Debug.WriteLine($"\n\n === === === CoursesViewModel === === === ");
            Debug.WriteLine($"LoadDataTest()");
            
        }
    }
}
