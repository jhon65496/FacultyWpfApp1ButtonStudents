using FacultyWpfApp1ButtonStudents.Models;
using FacultyWpfApp1ButtonStudents.ViewModels;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace FacultyWpfApp1ButtonStudents.Views
{
    /// <summary>
    /// Логика взаимодействия для SubjectsView.xaml
    /// </summary>
    public partial class SubjectsView : UserControl
    {
        private readonly ICollectionView subjectStudents;
        private readonly ICollectionView noSubjectStudents;
        private readonly MainWindowViewModel mainVM;
        public SubjectsView()
        {
            mainVM = ((MainWindowViewModel)FindResource(nameof(mainVM)));
            subjectStudents = ((CollectionViewSource)FindResource(nameof(subjectStudents))).View;
            noSubjectStudents = ((CollectionViewSource)FindResource(nameof(noSubjectStudents))).View;

            InitializeComponent();
        }

        private void OnSelectedSubjectChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainVM.SelectedSubject is not Subject subject)
            {
                subjectStudents.Filter = _ => false;
                noSubjectStudents.Filter = null;
            }
            else
            {
                subjectStudents.Filter = obj =>
                        obj is Student student &&
                        student.Subjects.Any(sbj => sbj.Id == subject.Id);
                noSubjectStudents.Filter = obj =>
                        obj is Student student &&
                        student.Subjects.All(sbj => sbj.Id != subject.Id);
            }
        }
    }
}
