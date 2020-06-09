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

namespace AcademicPerformance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
        
        
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void bntExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();           
        }

        private void bntRegistrtion_Click(object sender, RoutedEventArgs e)
        {
            WindowsFolder.WinRegistration winRegistration = 
                new WindowsFolder.WinRegistration();
            winRegistration.ShowDialog();
        }

        private void bntSigIn_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(CSqlHelper.CnnVal("AcademicPerformanceDB"))) {
                if (string.IsNullOrEmpty(TbLogin.Text))
                {
                    MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    TbLogin.Focus();
                }
                else if (string.IsNullOrEmpty(PbPassword.Password))
                {
                    MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    TbLogin.Focus();
                }
                else
                {
                    try
                    {
                        sqlConnection.Open();
                        sqlCommand = new SqlCommand("select PasswordUser, RoleUser,IdUser From dbo.[User] Where  LoginUser='" + TbLogin.Text + "'", sqlConnection);

                        // sqlCommand = new SqlCommand("Select PasswordUser,RoleUser from dbo.[User] Where LoginUser='" + TbLogin.Text + "'", sqlConnection);
                        sqlDataReader = sqlCommand.ExecuteReader();

                        if (!sqlDataReader.Read())
                        {
                            MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            TbLogin.Focus();
                        }
                        else if (sqlDataReader[0].ToString() != PbPassword.Password)
                        {
                            MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            PbPassword.Focus();
                        }
                        else
                        {
                            App.Login = TbLogin.Text;
                            App.Password = PbPassword.Password;
                            App.Role = sqlDataReader[1].ToString();
                            App.IdUser = sqlDataReader[2].ToString();
                            switch (App.Role)
                            {
                                case "1":
                                    WindowsFolder.WinAdmin winAdmin =
                                        new WindowsFolder.WinAdmin();
                                    winAdmin.ShowDialog();
                                    break;
                                case "2":
                                    WindowsFolder.WinDirector winDirector =
                                        new WindowsFolder.WinDirector();
                                    winDirector.ShowDialog();
                                    break;
                                case "3":
                                    WindowsFolder.WinManager winManager =
                                        new WindowsFolder.WinManager();
                                    winManager.ShowDialog();
                                    break;
                                case "4":
                                    WindowsFolder.WinTeacher winTeacher =
                                        new WindowsFolder.WinTeacher();
                                    winTeacher.ShowDialog();
                                    break;
                                case "5":
                                    WindowsFolder.WinStudent winStudent =
                                        new WindowsFolder.WinStudent();
                                    winStudent.ShowDialog();

                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                    finally
                    {

                    }
                }
            }
        }
    }
}
