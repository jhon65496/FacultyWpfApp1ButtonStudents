using FacultyWpfApp1ButtonStudents.Data;
using FacultyWpfApp1ButtonStudents.Models;
using FacultyWpfApp1ButtonStudents.Views;
using Simplified;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Input;

namespace FacultyWpfApp1ButtonStudents.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public DataContextApp dc;

        private readonly SubjectsViewModel subjectsViewModel;
        private readonly SubjectsStudentsJoinViewModel subjectsStudentsJoinViewModel;
        private readonly StudentsViewModel studentsViewModel;

        public IList<Student> Students => dc.Students;
        public IList<Subject> Subjects => dc.Subjects;

        //  public IList<Student> StudentsProxy;

        // public ICollectionView StudentsProxy;
          public CollectionViewSource StudentsProxy;
        // public ObservableCollection<Student> StudentsProxy;

        public IList<Student> NoStudentsProxy;

        // private readonly ICollectionView noSubjectStudents;

        public MainWindowViewModel()
        {
            this.dc = new DataContextApp();

            // SubjectsViewModel
            this.subjectsViewModel = new SubjectsViewModel(this);
            //subjectsViewModel.LoadDataTest();

            //SubjectsView cView = new SubjectsView();
            //cView.DataContext = subjectsViewModel;
            //SubjectsView = subjectsViewModel;

            // SubjectsStudentsJoinViewModel
            subjectsStudentsJoinViewModel = new SubjectsStudentsJoinViewModel(this);
            //subjectsStudentsJoinViewModel.LoadDataTest();

            //SubjectsStudentsJoinView csView = new SubjectsStudentsJoinView();
            //csView.DataContext = subjectsStudentsJoinViewModel;
            //SubjectsStudentsJoinView = subjectsStudentsJoinViewModel;

            // StudentsViewModel
            studentsViewModel = new StudentsViewModel(this.dc);
            //studentsViewModel.LoadDataTest();

            //StudentsView sView = new StudentsView();
            //sView.DataContext = studentsViewModel;
            //this.StudentsView = studentsViewModel;
            
            // noSubjectStudents = ((CollectionViewSource)FindResource(nameof(noSubjectStudents))).View;

            // Prop
            // this.SelectedSubject = subjectsViewModel.SelectedSubject;
        }

        public Student SelectedStudent { get => Get<Student>(); set => Set(value); }

        // SelectedSubject
        //private Subject selectedSubject;

        public Subject SelectedSubject { get => Get<Subject>(); set => Set(value); }
        //{
        //    get { return selectedSubject; }
        //    set
        //    {
        //        selectedSubject = value;
        //        Debug.WriteLine("\n\n === === === MainWindowViewModel.SelectedSubject === === ===");
        //        if (selectedSubject == null)
        //        {
        //            Debug.WriteLine($"SelectedSubject = null !!!");
        //            return;
        //        }
        //        Debug.WriteLine($"SelectedSubject.NameSubject -- {selectedSubject.Name}");

        //        // Установить критерий фильтрации для `SubjectsStudentsJoinViewModel`
        //        subjectsStudentsJoinViewModel.SubjectFilter = selectedSubject;

        //        // Установить критерий фильтрации для `StudentsViewModel`
        //        // var fdf = subjectsStudentsJoinViewModel.
        //        var cSJ = subjectsStudentsJoinViewModel.GetSubjectsStudentsJoin(selectedSubject);
        //        this.studentsViewModel.LoadDataUnion(cSJ);

        //        RaisePropertyChanged(nameof(SelectedSubject));
        //    }
        //}



        #region DeleteStudentCommand. Команда.
        private ICommand _deleteStudentCommand;
        public ICommand DeleteStudentCommand => _deleteStudentCommand ?? (_deleteStudentCommand = new RelayCommand(OnDeleteStudent));
        #endregion

        #region Секция методов
        private void OnDeleteStudent(object parameter)
        {
            var student = (Student)parameter;

            Students.Remove(student);
            SelectedSubject.Students.Remove(student);
        }
        #endregion
        /// <summary>
        /// View
        /// </summary>
        #region View === === === === === === === === ===
        // SubjectsView
        //private BaseVM subjectsView;

        public SubjectsViewModel SubjectsView { get => Get<SubjectsViewModel>(); set => Set(value); }
        //{
        //    get { return subjectsView; }
        //    set
        //    {
        //        subjectsView = value;
        //        RaisePropertyChanged(nameof(SubjectsView));
        //    }
        //}


        // SubjectStudentsView        
        //private BaseVM _сoursesStudentsJoinView;

        public SubjectsStudentsJoinViewModel SubjectsStudentsJoinView { get => Get<SubjectsStudentsJoinViewModel>(); set => Set(value); }
        //{
        //    get { return _сoursesStudentsJoinView; }
        //    set
        //    {
        //        _сoursesStudentsJoinView = value;
        //        RaisePropertyChanged(nameof(SubjectsStudentsJoinView));
        //    }
        //}

        // SubjectStudentsView
        //private BaseVM _studentsView;

        public StudentsViewModel StudentsView { get => Get<StudentsViewModel>(); set => Set(value); }
        //{
        //    get { return _studentsView; }
        //    set
        //    {
        //        _studentsView = value;
        //        RaisePropertyChanged(nameof(StudentsView));
        //    }
        //}



        #endregion
    }
}
