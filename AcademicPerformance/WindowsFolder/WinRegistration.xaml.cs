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

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinRegistration.xaml
    /// </summary>
    public partial class WinRegistration : Window
    {
        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
        SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal());
        SqlCommand sqlCommand;
        
        public WinRegistration()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TbLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                TbLogin.Focus();
            }
            else if(string.IsNullOrEmpty(PbPassword.Password))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                PbPassword.Focus();
            }
            else if(string.IsNullOrEmpty(PbPasswordRepeat.Password))
            {
                MessageBox.Show("Повторите пароль", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                PbPasswordRepeat.Focus();
            }
            else if(PbPasswordRepeat.Password!=PbPassword.Password)
            {
                MessageBox.Show("Пароли должны совпадать", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                PbPasswordRepeat.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert into dbo.[User] (LoginUser,PasswordUser,RoleUser)" +
                        "Values (@LoginUser,@PasswordUser,@RoleUser)", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("LoginUser", TbLogin.Text);
                    sqlCommand.Parameters.AddWithValue("PasswordUser", PbPassword.Password);
                    sqlCommand.Parameters.AddWithValue("RoleUser", "5");
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Вы успешно зарегестрировались", "Информация", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка",
                       MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
