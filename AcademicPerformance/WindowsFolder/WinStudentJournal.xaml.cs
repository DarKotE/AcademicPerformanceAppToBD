using System;
using System.Collections.Generic;
using System.Data;
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
using AcademicPerformance.ClassFolder;
using AcademicPerformance.ViewModelFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinStudent.xaml
    /// </summary>
    public partial class WinStudent : Window
    {
        public WinStudent()
        {            
            InitializeComponent();
            var studentJournal = new VMStudentJournal();
            this.DataContext = studentJournal;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbSearch.Focus();
        }
        
        
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно желаете выйти?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }
        }

        private void MiPersonalProfile_Click(object sender, RoutedEventArgs e)
        {
            WinProfileStudent winProfileStudent = new WinProfileStudent();
            winProfileStudent.ShowDialog();
        }

        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        
    }
}
