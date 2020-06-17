using System.Windows;
using AcademicPerformance.ViewModelsFolder;
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
            App.PasswordUser = PbPasswordRepeat.Password;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbLogin.Focus();
            TbLogin.SelectAll();
        }
    }
}
