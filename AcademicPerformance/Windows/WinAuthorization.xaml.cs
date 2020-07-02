using System.Threading.Tasks;
using System.Windows;
using AcademicPerformance.Classes;
using AcademicPerformance.ViewModels;

namespace AcademicPerformance.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
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
                case Const.RoleValue.User:
                    WinProfileStudent winUser = new WinProfileStudent();
                    winUser.ShowDialog();
                    break;
                case Const.RoleValue.Director:
                    WinDirector winDirector = new WinDirector();
                    winDirector.ShowDialog();
                    break;
                case Const.RoleValue.Admin:
                    WinAdmin winAdmin = new WinAdmin();
                    winAdmin.ShowDialog();
                    break;
                case Const.RoleValue.Student:
                    WinStudent winStudent = new WinStudent();
                    winStudent.ShowDialog();
                    break;
                case Const.RoleValue.Teacher:
                    WinTeacher winTeacher = new WinTeacher();
                    winTeacher.ShowDialog();
                    break;
                case Const.RoleValue.Manager:
                    WinAdminJournal winManager = new WinAdminJournal();
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
            WinRegistration winRegistration = new WinRegistration();
            winRegistration.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbLogin.Focus();
            TbLogin.SelectAll();
        }


    }
}
    



