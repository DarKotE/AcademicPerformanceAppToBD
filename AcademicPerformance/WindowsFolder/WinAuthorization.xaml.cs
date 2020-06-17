using System.Threading.Tasks;
using System.Windows;
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       private void RefreshView()
        {
            var authorization = new VMAuthorization();
            this.DataContext = null;
            this.DataContext = authorization;
        }


        public MainWindow()
        {
            InitializeComponent();
            RefreshView();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private bool IsTextboxFilled()
        {
            if (string.IsNullOrEmpty(TbLogin.Text))
            {
                TbLogin.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(PbPassword.Password))
            {
                PbPassword.Focus();
                return false;
            }
            return true;
        }

        private void ShowNextWindow(int i)
        {
            switch (i)
            {
                case 1:
                    WindowsFolder.WinProfileStudent winUser = new WindowsFolder.WinProfileStudent();
                    winUser.ShowDialog();
                    break;
                case 2:
                    WindowsFolder.WinDirector winDirector = new WindowsFolder.WinDirector();
                    winDirector.ShowDialog();
                    break;
                case 3:
                    WindowsFolder.WinAdmin winAdmin = new WindowsFolder.WinAdmin();
                    winAdmin.ShowDialog();
                    break;
                case 4:
                    WindowsFolder.WinStudent winStudent = new WindowsFolder.WinStudent();
                    winStudent.ShowDialog();
                    break;
                case 5:
                    WindowsFolder.WinTeacher winTeacher = new WindowsFolder.WinTeacher();
                    winTeacher.ShowDialog();
                    break;
                case 6:
                    WindowsFolder.WinAdminJournal winManager = new WindowsFolder.WinAdminJournal();
                    winManager.ShowDialog();
                    break;
            }
        }

        private async void BntSignIn_Click(object sender, RoutedEventArgs e)
        {
            IsTextboxFilled();
            await Task.Delay(100);
            
            ShowNextWindow(App.RoleUser);

        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            WindowsFolder.WinRegistration winRegistration = new WindowsFolder.WinRegistration();
            winRegistration.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbLogin.Focus();
            TbLogin.SelectAll();
        }


    }
}
    



