using FacultyWpfApp1ButtonStudents.Commands;
using FacultyWpfApp1ButtonStudents.Data;
using FacultyWpfApp1ButtonStudents.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace FacultyWpfApp1ButtonStudents.ViewModels
{
    class CoursesStudentsJoinViewModel : BaseVM
    {
        MainWindowViewModel mainWindowViewModel;

        DataContextApp dc;
        // ctor
        public CoursesStudentsJoinViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.dc = this.mainWindowViewModel.dc;
        }


        // CoursesStudentsJoins
        private ObservableCollection<CourseStudentJoin> _coursesStudentsJoin;

        public ObservableCollection<CourseStudentJoin> CoursesStudentsJoins
        {
            get { return _coursesStudentsJoin; }
            set
            {
                _coursesStudentsJoin = value;

                _CoursesStudentsJoinsViewSource = new CollectionViewSource();
                _CoursesStudentsJoinsViewSource.Source = value;
                _CoursesStudentsJoinsViewSource.Filter += OnCoursesStudentsJoinsFilter;
                _CoursesStudentsJoinsViewSource.View.Refresh(); // 

                // RaisePropertyChanged(nameof(CoursesStudentsJoins));
                // CoursesStudentsJoinsView
                RaisePropertyChanged(nameof(CoursesStudentsJoinsView));
            }
        }


        // SelectedCourse
        private CourseStudentJoin _selectedCourseStudentJoin;

        public CourseStudentJoin SelectedCoursesStudents
        {
            get { return _selectedCourseStudentJoin; }
            set
            {
                _selectedCourseStudentJoin = value;


                //if (_selectedCourseStudentJoin == null) return;
                //Debug.WriteLine($"--- --- --- --- --- --- --- --- ---");
                //Debug.WriteLine($"IndexesViewModel--selectedIndexCalculation -- {_selectedCourseStudentJoin.NameCourse}");
                //if (this._selectedCourseStudentJoin == null)
                //{
                //    Debug.WriteLine($"IndexesViewModel--selectedIndexCalculation -- managerIndexesViewModel = null\n");
                //    return;
                //}

                // this.managerIndexesViewModel.SelectedIndexCalculation = selectedIndexCalculation;


                RaisePropertyChanged(nameof(SelectedCoursesStudents));
            }
        }


        #region Filter == === === === === ==
        private Course _сourseFilter;

        public Course CourseFilter
        {
            get { return _сourseFilter; }
            set
            {
                _сourseFilter = value;

                // Debug.WriteLine("\n\n=== === === CoursesStudentsJoinViewModel === === ===");

                if (CourseFilter == null)
                {
                    // Debug.WriteLine($"CourseFilter.NameCourse -- Null !!!!");
                    return;
                }
                // Debug.WriteLine($"CourseFilter.NameCourse -- {CourseFilter.NameCourse}");

                _CoursesStudentsJoinsViewSource.View.Refresh();
            }
        }


        private void OnCoursesStudentsJoinsFilter(object sender, FilterEventArgs e)
        {
            // Debug.WriteLine($"\n\n === === === CoursesStudentsJoinViewModel === === === ");
            // Debug.WriteLine($"OnIndexProvidersFilter(object sender, FilterEventArgs e) ");


            if (!(e.Item is CourseStudentJoin courseStudentJoin)) return;


            if (CourseFilter == null) return;

            // Debug.WriteLine($"courseStudentJoin.IdCourse == CourseFilter.IdCourse -- {courseStudentJoin.IdCourse} = {CourseFilter.IdCourse} ");
            if (courseStudentJoin.IdCourse == CourseFilter.Id)
            {
                e.Accepted = true;
                //  Debug.WriteLine($"e.Accepted = true");
            }
            else
            {
                e.Accepted = false;
                // Debug.WriteLine($"e.Accepted = false");
            }
        }

        #region CollectionView
        private CollectionViewSource _CoursesStudentsJoinsViewSource;

        public ICollectionView CoursesStudentsJoinsView => _CoursesStudentsJoinsViewSource?.View;
        #endregion

        #endregion

        #region Command TestCommand - Тестовая команда
        /// <summary>Тестовая команда</summary>
        private ICommand _TestCommand;

        /// <summary>Тестовая команда</summary>
        public ICommand TestCommand
        {
            get
            {
                if (_TestCommand == null)
                {
                    _TestCommand = new LambdaCommand(OnTestCommandExecuted, CanTestCommandExecute);
                }
                return _TestCommand;
            }
        }

        /// <summary>Проверка возможности выполнения - Тестовая команда</summary>
        private bool CanTestCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Тестовая команда</summary>
        private void OnTestCommandExecuted(object p)
        {
            //var value = _UserDialog.GetStringValue("Введите строку", "123", "Значение по умолчанию");
            //_UserDialog.ShowInformation($"Введено: {value}", "123");

            var df = (CourseStudentJoin)p;

            Debug.WriteLine("\n\n === === === CoursesStudentsJoinViewModel === === ===");
            Debug.WriteLine($"OnTestCommandExecuted(object p) -- p.NameStudent -- {df.NameStudent}");
        }
        #endregion


        public void LoadDataTest()
        {
            // СalculationIndexs = this.DataContextApp.СalculationIndexes;
            CoursesStudentsJoins = dc.CoursesStudentsJoins;
            // return dc.Courses;

            Debug.WriteLine($"\n\n === === === CoursesStudentsJoinViewModel === === === ");
            Debug.WriteLine($"LoadDataTest() ");
            // Debug.WriteLine($"СalculationIndexs.Count -- {СalculationIndexs.Count}");
        }

        public ObservableCollection<CourseStudentJoin> GetCoursesStudentsJoin(Course course)
        {
            int IdCourse = course.Id;

            var res = CoursesStudentsJoins.Where(cSJ => cSJ.IdCourse == IdCourse).ToList();
            var coursesStudentsJoins = new ObservableCollection<CourseStudentJoin>(res);

            return coursesStudentsJoins;

            Debug.WriteLine($"\n\n === === === CoursesStudentsJoinViewModel === === === ");
            Debug.WriteLine($"LoadDataTest() ");
            // Debug.WriteLine($"СalculationIndexs.Count -- {СalculationIndexs.Count}");
        }
    }
}
