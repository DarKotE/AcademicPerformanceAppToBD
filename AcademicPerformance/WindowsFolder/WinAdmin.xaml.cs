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
    /// Interaction logic for WinAdmin.xaml
    /// </summary>
    public partial class WinAdmin : Window
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
            WindowsFolder.WinAdminJournal winAdminJournal = new WindowsFolder.WinAdminJournal();
            winAdminJournal.ShowDialog();
        }

        private void miTeacherWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miStudentWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miManagerWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miDirectorWindow_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
