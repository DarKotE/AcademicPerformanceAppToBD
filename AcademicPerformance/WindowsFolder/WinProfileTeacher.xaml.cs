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
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinProfileTeacher.xaml
    /// </summary>
    public partial class WinProfileTeacher : Window
    {
        public Delegate UpdateActor;
        public WinProfileTeacher()
        {
            InitializeComponent();
            var profileTeacher = new VMProfileTeacher();
            this.DataContext = profileTeacher;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            UpdateActor.DynamicInvoke();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            UpdateActor.DynamicInvoke();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
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