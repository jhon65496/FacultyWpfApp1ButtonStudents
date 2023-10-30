using FacultyWpfApp1ButtonStudents.Models;
using FacultyWpfApp1ButtonStudents.ViewModels;
using Simplified;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace FacultyWpfApp1ButtonStudents
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly ICollectionView subjectStudents;
        //private readonly ICollectionView noSubjectStudents;
        //private readonly MainWindowViewModel mainVM;

        public MainWindow()
        {
            //mainVM = ((MainWindowViewModel)FindResource(nameof(mainVM)));
            //subjectStudents = ((CollectionViewSource)FindResource(nameof(subjectStudents))).View;
            //noSubjectStudents = ((CollectionViewSource)FindResource(nameof(noSubjectStudents))).View;

            InitializeComponent();
        }

        //private void OnSelectedSubjectChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (mainVM.SelectedSubject is not Subject subject)
        //    {
        //        subjectStudents.Filter = _ => false;
        //        noSubjectStudents.Filter = null;
        //    }
        //    else
        //    {
        //        subjectStudents.Filter = obj =>
        //                obj is Student student &&
        //                student.Subjects.Any(sbj => sbj.Id == subject.Id);
        //        noSubjectStudents.Filter = obj =>
        //                obj is Student student &&
        //                student.Subjects.All(sbj => sbj.Id != subject.Id);
        //    }
        //}

    }
}
