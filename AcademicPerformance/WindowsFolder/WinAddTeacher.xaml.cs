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

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для WinAddTeacher.xaml
    /// </summary>
    public partial class WinAddTeacher : Window
    {
        public WinAddTeacher()
        {
            InitializeComponent();
        }

        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
        SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal());
        SqlCommand sqlCommand;
        SqlDataAdapter dataAdapter;
        SqlDataReader sqlDataReader;
        DataGrid dataGrid;
        DataTable dataTable;


        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbTeacherLastName.Text))
            {
                MessageBox.Show("Введите фамилию студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbTeacherLastName.Focus();
            }
            else if (string.IsNullOrEmpty(tbTeacherName.Text))
            {
                MessageBox.Show("Введите имя студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbTeacherName.Focus();
            }
            else if (string.IsNullOrEmpty(dpTeacherDateOfBirth.Text))
            {
                MessageBox.Show("Введите дату рождения студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dpTeacherDateOfBirth.Focus();
            }
            else if (string.IsNullOrEmpty(tbTeacherNumberPhone.Text))
            {
                MessageBox.Show("Введите номер телефона студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbTeacherNumberPhone.Focus();
            }
            else if (string.IsNullOrEmpty(tbTeacherLogin.Text))
            {
                MessageBox.Show("Введите логин для студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbTeacherLogin.Focus();
            }
            else if (tbTeacherLogin.Text != "")
            {
                if (string.IsNullOrEmpty(tbTeacherPassword.Text))
                {
                    MessageBox.Show("Введите пароль", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        sqlConnection.Open();
                        sqlCommand = new SqlCommand("Insert Into dbo.[Teacher] (LastNameTeacher, FirstNameTeacher, MiddleNameTeacher, DateOfBirthTeacher, NumberPhoneTeacher, IdUser) " +
                            "Values (@LastNameTeacher, @FirstNameTeacher, @MiddleNameTeacher, @DateOfBirthTeacher, @NumberPhoneTeacher,@IdUser)", sqlConnection);

                        sqlCommand.Parameters.AddWithValue("LastNameTeacher", tbTeacherLastName.Text);
                        sqlCommand.Parameters.AddWithValue("FirstNameTeacher", tbTeacherName.Text);
                        sqlCommand.Parameters.AddWithValue("MiddleNameTeacher", tbStudMiddleName.Text);
                        sqlCommand.Parameters.AddWithValue("DateOfBirthTeacher", dpTeacherDateOfBirth.Text);
                        sqlCommand.Parameters.AddWithValue("NumberPhoneTeacher", tbTeacherNumberPhone.Text);
                        sqlCommand.Parameters.AddWithValue("IdUser", "5");
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand = new SqlCommand("Insert Into dbo.[User] " +
                            "(LoginUser, PasswordUser, RoleUser) Values (@LoginUser, @PasswordUser, @RoleUser)", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("LoginUser", tbTeacherLogin.Text);
                        sqlCommand.Parameters.AddWithValue("PasswordUser", tbTeacherPassword.Text);
                        sqlCommand.Parameters.AddWithValue("RoleUser", "5");
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("Успешно добавлено", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }

                }

            }

        }




    }
}
