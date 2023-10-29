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
        private readonly ICollectionView students;
        private readonly MainWindowViewModel mainVM;
        public SubjectsView()
        {
            mainVM = ((MainWindowViewModel)FindResource(nameof(mainVM)));
            students = ((CollectionViewSource)FindResource(nameof(students))).View;

            InitializeComponent();
        }

        private void OnSelectedSubjectChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainVM.SelectedSubject is not Subject subject)
            {
                students.Filter = null;
            }
            else
            {
                students.Filter = obj =>
                        obj is Student student &&
                        student.Subjects.Any(sbj => sbj.Id == subject.Id);
            }
        }
    }
}
