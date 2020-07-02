using System.Windows;
using AcademicPerformance.Classes;

namespace AcademicPerformance.Views
{
    /// <summary>
    /// Interaction logic for WinDirector.xaml
    /// </summary>
    public partial class WinDirector 
    {
        public WinDirector()
        {
            InitializeComponent();
        }

        private void bntExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnJournal_Click(object sender, RoutedEventArgs e)
        {
            var winAdminJournal = new WinAdminJournal();
            winAdminJournal.ShowDialog();
        }


        private void miTeacherWindow_Click(object sender, RoutedEventArgs e)
        {
            App.RoleUser = Const.RoleValue.Teacher;
            var winTeacher = new WinTeacher();
            winTeacher.ShowDialog();
        }

        private void miStudentWindow_Click(object sender, RoutedEventArgs e)
        {
            App.RoleUser = Const.RoleValue.Student;
            var winStudent = new WinStudent();
            winStudent.ShowDialog();
        }

        private void miManagerWindow_Click(object sender, RoutedEventArgs e)
        {
            App.RoleUser = Const.RoleValue.Manager;
            var winManager = new WinAdminJournal();
            winManager.ShowDialog();
        }
        
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void miAddStudentProfile_Click(object sender, RoutedEventArgs e)
        {
            var winDirector = new WinAddStudent();
            winDirector.ShowDialog();
        }
        private void miAddTeacherProfile_Click(object sender, RoutedEventArgs e)
        {
            var winDirector = new WinAddTeacher();
            winDirector.ShowDialog();
        }

        private void miAddDiscipline_Click(object sender, RoutedEventArgs e)
        {
            var winAddDiscipline = new WinAddDiscipline();
            winAddDiscipline.ShowDialog();
        }
    }



}
