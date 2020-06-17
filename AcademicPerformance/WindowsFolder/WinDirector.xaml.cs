using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Configuration;
namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinDirector.xaml
    /// </summary>
    public partial class WinDirector : Window
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

        private void miStudentProfile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void miTeacherProfile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void miManagerProfile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void miDirectorProfile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }



}
