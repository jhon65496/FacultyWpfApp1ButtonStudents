using FacultyWpfApp1ButtonStudents.Data;
using FacultyWpfApp1ButtonStudents.Models;
using Simplified;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FacultyWpfApp1ButtonStudents.ViewModels
{
    public class SubjectsViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        private readonly DataContextApp dc;
        // ctor
        public SubjectsViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            this.dc = this.mainWindowViewModel.dc;
        }

        // Конструктор Времени Разработки
        public SubjectsViewModel() : this(new MainWindowViewModel()) { }


        // Subjects
        private ObservableCollection<Subject> _subjects;

        public ObservableCollection<Subject> Subjects
        {
            get { return _subjects; }
            set
            {
                _subjects = value;



                RaisePropertyChanged(nameof(Subjects));
            }
        }


        // SelectedSubject
        //private Subject _selectedSubject;

        public Subject SelectedSubject { get => Get<Subject>(); set => Set(value); }
        //{
        //    get { return _selectedSubject; }
        //    set
        //    {
        //        _selectedSubject = value;
        //        Debug.WriteLine("\n\n === === === SubjectsViewModel.SelectedSubject === === ===");
        //        if (SelectedSubject == null)
        //        {
        //            Debug.WriteLine($"SelectedSubject = null !!!");
        //            return;
        //        }
        //        Debug.WriteLine($"SelectedSubject.NameSubject -- {SelectedSubject.Name}");
        //        this.mainWindowViewModel.SelectedSubject = SelectedSubject;
        //        RaisePropertyChanged(nameof(SelectedSubject));
        //    }
        //}


        public void LoadDataTest()
        {
            // СalculationIndexs = this.DataContextApp.СalculationIndexes;
            Subjects = dc.Subjects;
            // return dc.Subjects;

            Debug.WriteLine($"\n\n === === === SubjectsViewModel === === === ");
            Debug.WriteLine($"LoadDataTest()");

        }
    }
}
