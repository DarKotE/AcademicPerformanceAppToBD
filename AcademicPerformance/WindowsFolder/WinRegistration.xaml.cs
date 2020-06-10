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
using AcademicPerformance.ClassFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinRegistration.xaml
    /// </summary>
    public partial class WinRegistration : Window
    {
        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
        //SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal());
        //SqlCommand sqlCommand;
        private readonly CDataAccess dataAccess = new CDataAccess();

        public WinRegistration()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        private bool IsRegistrationRight()
        {
            if (string.IsNullOrEmpty(TbLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
                TbLogin.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(PbPassword.Password))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
                PbPassword.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(PbPasswordRepeat.Password))
            {
                MessageBox.Show("Повторите пароль", "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
                PbPasswordRepeat.Focus();
                return false;
            }
            else if (PbPasswordRepeat.Password != PbPassword.Password)
            {
                MessageBox.Show("Пароли должны совпадать", "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
                PbPasswordRepeat.Focus();
                return false;
            }
            else
                return true;


        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (IsRegistrationRight()) 
            {
                if (!dataAccess.IsLoginFree(TbLogin.Text))
                {
                    MessageBox.Show("Данный логин уже занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    CUser user = new CUser();
                    user.LoginUser = TbLogin.Text;
                    user.PasswordUser = PbPassword.Password;
                    user.RoleUser = 5;

                    dataAccess.InsertUser(user, out bool isErr);
                    if (!isErr)
                    {
                        MessageBox.Show("Вы успешно зарегестрировались", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("В ходе регистрации произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    
                }
            }
            
            
        }
    }
}
