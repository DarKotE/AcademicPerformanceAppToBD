using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Forms;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.ViewModelFolder;
using Application = System.Windows.Application;


namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinRegistration.xaml
    /// </summary>
    public partial class WinRegistration : Window
    {
        public WinRegistration()
        {
            InitializeComponent();
            var registration = new VMRegistration();
            this.DataContext = registration;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }



        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            //bad
            App.PasswordUser = PbPasswordRepeat.Password;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbLogin.Focus();
            TbLogin.SelectAll();
        }
    }
}
