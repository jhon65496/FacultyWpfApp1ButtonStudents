using FacultyWpfApp1ButtonStudents.Data;
using FacultyWpfApp1ButtonStudents.Models;
using Simplified;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;

namespace FacultyWpfApp1ButtonStudents.ViewModels
{
    public class SubjectsStudentsJoinViewModel : ViewModelBase
    {
        MainWindowViewModel mainWindowViewModel;

        DataContextApp dc;
        // ctor
        public SubjectsStudentsJoinViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.dc = this.mainWindowViewModel.dc;
        }


        // SubjectsStudentsJoins
        private ObservableCollection<SubjectStudentJoin> _subjectsStudentsJoin;

        public ObservableCollection<SubjectStudentJoin> SubjectsStudentsJoins
        {
            get { return _subjectsStudentsJoin; }
            set
            {
                _subjectsStudentsJoin = value;

                _SubjectsStudentsJoinsViewSource = new CollectionViewSource();
                _SubjectsStudentsJoinsViewSource.Source = value;
                _SubjectsStudentsJoinsViewSource.Filter += OnSubjectsStudentsJoinsFilter;
                _SubjectsStudentsJoinsViewSource.View.Refresh(); // 

                // RaisePropertyChanged(nameof(SubjectsStudentsJoins));
                // SubjectsStudentsJoinsView
                RaisePropertyChanged(nameof(SubjectsStudentsJoinsView));
            }
        }


        // SelectedSubject
        //private SubjectStudentJoin _selectedSubjectStudentJoin;

        public SubjectStudentJoin SelectedSubjectsStudents { get => Get<SubjectStudentJoin>(); set => Set(value); }
        //{
        //    get { return _selectedSubjectStudentJoin; }
        //    set
        //    {
        //        _selectedSubjectStudentJoin = value;


        //        //if (_selectedSubjectStudentJoin == null) return;
        //        //Debug.WriteLine($"--- --- --- --- --- --- --- --- ---");
        //        //Debug.WriteLine($"IndexesViewModel--selectedIndexCalculation -- {_selectedSubjectStudentJoin.NameSubject}");
        //        //if (this._selectedSubjectStudentJoin == null)
        //        //{
        //        //    Debug.WriteLine($"IndexesViewModel--selectedIndexCalculation -- managerIndexesViewModel = null\n");
        //        //    return;
        //        //}

        //        // this.managerIndexesViewModel.SelectedIndexCalculation = selectedIndexCalculation;


        //        RaisePropertyChanged(nameof(SelectedSubjectsStudents));
        //    }
        //}


        #region Filter == === === === === ==
        private Subject _сourseFilter;

        public Subject SubjectFilter
        {
            get { return _сourseFilter; }
            set
            {
                _сourseFilter = value;

                // Debug.WriteLine("\n\n=== === === SubjectsStudentsJoinViewModel === === ===");

                if (SubjectFilter == null)
                {
                    // Debug.WriteLine($"SubjectFilter.NameSubject -- Null !!!!");
                    return;
                }
                // Debug.WriteLine($"SubjectFilter.NameSubject -- {SubjectFilter.NameSubject}");

                _SubjectsStudentsJoinsViewSource.View.Refresh();
            }
        }


        private void OnSubjectsStudentsJoinsFilter(object sender, FilterEventArgs e)
        {
            // Debug.WriteLine($"\n\n === === === SubjectsStudentsJoinViewModel === === === ");
            // Debug.WriteLine($"OnIndexProvidersFilter(object sender, FilterEventArgs e) ");


            if (!(e.Item is SubjectStudentJoin subjectStudentJoin)) return;


            if (SubjectFilter == null) return;

            // Debug.WriteLine($"subjectStudentJoin.IdSubject == SubjectFilter.IdSubject -- {subjectStudentJoin.IdSubject} = {SubjectFilter.IdSubject} ");
            if (subjectStudentJoin.IdSubject == SubjectFilter.Id)
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
        private CollectionViewSource _SubjectsStudentsJoinsViewSource;

        public ICollectionView SubjectsStudentsJoinsView => _SubjectsStudentsJoinsViewSource?.View;
        #endregion

        #endregion

        #region Command TestCommand - Тестовая команда
        /// <summary>Тестовая команда</summary>
        //private ICommand _TestCommand;

        /// <summary>Тестовая команда</summary>
        public RelayCommand TestCommand => GetCommand(OnTestCommandExecuted, CanTestCommandExecute);
        //{
        //    get
        //    {
        //        if (_TestCommand == null)
        //        {
        //            _TestCommand = new LambdaCommand(OnTestCommandExecuted, CanTestCommandExecute);
        //        }
        //        return _TestCommand;
        //    }
        //}

        /// <summary>Проверка возможности выполнения - Тестовая команда</summary>
        private bool CanTestCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Тестовая команда</summary>
        private void OnTestCommandExecuted(object p)
        {
            //var value = _UserDialog.GetStringValue("Введите строку", "123", "Значение по умолчанию");
            //_UserDialog.ShowInformation($"Введено: {value}", "123");

            var df = (SubjectStudentJoin)p;

            Debug.WriteLine("\n\n === === === SubjectsStudentsJoinViewModel === === ===");
            Debug.WriteLine($"OnTestCommandExecuted(object p) -- p.NameStudent -- {df.NameStudent}");
        }
        #endregion


        public void LoadDataTest()
        {
            // СalculationIndexs = this.DataContextApp.СalculationIndexes;
            SubjectsStudentsJoins = dc.SubjectStudentsJoins;
            // return dc.Subjects;

            Debug.WriteLine($"\n\n === === === SubjectsStudentsJoinViewModel === === === ");
            Debug.WriteLine($"LoadDataTest() ");
            // Debug.WriteLine($"СalculationIndexs.Count -- {СalculationIndexs.Count}");
        }

        public ObservableCollection<SubjectStudentJoin> GetSubjectsStudentsJoin(Subject subject)
        {
            int IdSubject = subject.Id;

            var res = SubjectsStudentsJoins.Where(cSJ => cSJ.IdSubject == IdSubject).ToList();
            var subjectsStudentsJoins = new ObservableCollection<SubjectStudentJoin>(res);

            return subjectsStudentsJoins;

            Debug.WriteLine($"\n\n === === === SubjectsStudentsJoinViewModel === === === ");
            Debug.WriteLine($"LoadDataTest() ");
            // Debug.WriteLine($"СalculationIndexs.Count -- {СalculationIndexs.Count}");
        }
    }
}
