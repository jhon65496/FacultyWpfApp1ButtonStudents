using FacultyWpfApp1ButtonStudents.Data;
using FacultyWpfApp1ButtonStudents.Models;
using Simplified;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;


namespace FacultyWpfApp1ButtonStudents.ViewModels
{
    public class StudentsViewModel : ViewModelBase
    {
        // MainWindowViewModel mainWindowViewModel;

        private readonly DataContextApp dc;
        // ctor
        public StudentsViewModel(DataContextApp dc)
        {
            //  this.mainWindowViewModel = mainWindowViewModel;

            this.dc = dc;
        }


        // Subjects
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



        // SelectedSubject
        //private Student _selectedStudent;

        public Student SelectedStudent { get => Get<Student>(); set => Set(value); }
        //{
        //    get { return _selectedStudent; }
        //    set
        //    {
        //        _selectedStudent = value;
        //        Debug.WriteLine("\n\n === === === SubjectsViewModel.SelectedSubject === === ===");                
        //        if (SelectedStudent == null)
        //        {
        //            Debug.WriteLine($"SelectedSubject = null !!!");
        //            return;
        //        }
        //        Debug.WriteLine($"SelectedSubject.NameSubject -- {SelectedStudent.Name}");
        //        // this.mainWindowViewModel.SelectedSubject = SelectedStudent;
        //        RaisePropertyChanged(nameof(SelectedStudent));
        //    }
        //}


        public void LoadDataTest()
        {
            // СalculationIndexs = this.DataContextApp.СalculationIndexes;
            Students = dc.Students;
            // return dc.Subjects;

            Debug.WriteLine($"\n\n === === === SubjectsViewModel === === === ");
            Debug.WriteLine($"LoadDataTest()");

        }

        public void LoadDataUnion(ObservableCollection<SubjectStudentJoin> subjectStudentJoin)
        {

            // var result = Students.Where(cSJ => !subjectStudentJoin
            //                     .Select(s => s.IdStudent).Contains(cSJ.IdStudent));

            StudentsView = new ObservableCollection<Student>(Students
                                .Where(cSJ => !subjectStudentJoin
                                    .Select(s => s.IdStudent).Contains(cSJ.Id)));

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
