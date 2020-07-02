using System.Windows;

namespace AcademicPerformance.Views
{
    /// <summary>
    ///     Interaction logic for WinAdmin.xaml
    /// </summary>
    public partial class WinAdmin 
    {
        public WinAdmin()
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
            App.RoleUser = 5;
            var winTeacher = new WinTeacher();
            winTeacher.ShowDialog();
        }

        private void miStudentWindow_Click(object sender, RoutedEventArgs e)
        {
            App.RoleUser = 4;
            var winStudent = new WinStudent();
            winStudent.ShowDialog();
        }

        private void miManagerWindow_Click(object sender, RoutedEventArgs e)
        {
            App.RoleUser = 6;
            var winManager = new WinAdminJournal();
            winManager.ShowDialog();
        }

        private void miDirectorWindow_Click(object sender, RoutedEventArgs e)
        {
            App.RoleUser = 2;
            var winDirector = new WinDirector();
            winDirector.ShowDialog();
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

        private void btnUsersRoles_Click(object sender, RoutedEventArgs e)
        {
            var winRoleEdit = new WinRoleEdit();
            winRoleEdit.ShowDialog();
        }
    }
}