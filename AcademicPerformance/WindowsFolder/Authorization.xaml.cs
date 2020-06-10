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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using AcademicPerformance.ClassFolder;

namespace AcademicPerformance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CDataAccess dataAccess = new CDataAccess();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnRegistartion_Click(object sender, RoutedEventArgs e)
        {
            WindowsFolder.WinRegistration winRegistration = new WindowsFolder.WinRegistration();
            winRegistration.ShowDialog();
        }

        private bool IsTextboxFilled()
        {

            if (string.IsNullOrEmpty(TbLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                TbLogin.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(PbPassword.Password))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                TbLogin.Focus();
                return false;
            }
            return true;
        }
        private void ShowNextWindow()
        {
            switch (App.RoleUser)
            {
                case 1:
                    WindowsFolder.WinAdmin winAdmin = new WindowsFolder.WinAdmin();
                    winAdmin.ShowDialog();
                    break;
                case 2:
                    WindowsFolder.WinDirector winDirector = new WindowsFolder.WinDirector();
                    winDirector.ShowDialog();
                    break;
                case 3:
                    WindowsFolder.WinManager winManager = new WindowsFolder.WinManager();
                    winManager.ShowDialog();
                    break;
                case 4:
                    WindowsFolder.WinTeacher winTeacher = new WindowsFolder.WinTeacher();
                    winTeacher.ShowDialog();
                    break;
                case 5:
                    WindowsFolder.WinStudent winStudent = new WindowsFolder.WinStudent();
                    winStudent.ShowDialog();
                    break;
            }
        }

        private void BntSigIn_Click(object sender, RoutedEventArgs e)
        {
            if (IsTextboxFilled())
            {
                if (!dataAccess.IsAuthValid(TbLogin.Text, PbPassword.Password))
                {
                    MessageBox.Show("Логин или пароль не верны, проверьте введённые данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    _ = new CUser();
                    CUser user = dataAccess.GetUser(TbLogin.Text, PbPassword.Password);
                    App.LoginUser = user.LoginUser;
                    App.PasswordUser = user.PasswordUser;
                    App.IdUser = user.IdUser;
                    App.RoleUser = user.RoleUser;
                    ShowNextWindow();
                }
            }
        }
    }
}
    



