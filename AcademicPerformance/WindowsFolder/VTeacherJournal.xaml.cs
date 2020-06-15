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
using AcademicPerformance.ViewModelFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinTeacher.xaml
    /// </summary>
    public partial class WinTeacher : Window
    {
        public WinTeacher()
        {
            InitializeComponent();
            var teacherJournal = new VMTeacherJournal();
            this.DataContext = teacherJournal;
            
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
        

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            WinAdd winAdd = new WinAdd();
            winAdd.ShowDialog();
        }

        private void miProfile_Click(object sender, RoutedEventArgs e)
        {
            WinProfileTeacher winProfileTeacher = new WinProfileTeacher();
            winProfileTeacher.ShowDialog();
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно желаете выйти?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }
        }
    }
}


