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
    /// Логика взаимодействия для WinAddStudent.xaml
    /// </summary>
    public partial class WinAddStudent : Window
    {
        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
        SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal());
        SqlCommand sqlCommand;
        SqlDataAdapter dataAdapter;
        SqlDataReader sqlDataReader;
        DataGrid dataGrid;
        DataTable dataTable;
        
       
        public WinAddStudent()
        {
            InitializeComponent();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbStudLastName.Text))
            {
                MessageBox.Show("Введите фамилию студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudLastName.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudName.Text))
            {
                MessageBox.Show("Введите имя студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudName.Focus();
            }
            else if (string.IsNullOrEmpty(dpStudDateOfBirth.Text))
            {
                MessageBox.Show("Введите дату рождения студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dpStudDateOfBirth.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudNumberPhone.Text))
            {
                MessageBox.Show("Введите номер телефона студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudNumberPhone.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudLogin.Text))
            {
                MessageBox.Show("Введите логин для студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudLogin.Focus();                
            }
            else if (tbStudLogin.Text != "")
            {
                if (string.IsNullOrEmpty(tbStudPassword.Password))
                {
                    MessageBox.Show("Введите пароль", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        sqlConnection.Open();
                        sqlCommand = new SqlCommand("Insert Into dbo.[Student] (LastNameStudent, FirstNameStudent, MiddleNameStudent, DateOfBirthStudent, NumberPhoneStudent) " +
                            "Values (@LastNameStudent, @FirstNameStudent, @MiddleNameStudent, @DateOfBirthStudent, @NumberPhoneStudent)", sqlConnection);

                        sqlCommand.Parameters.AddWithValue("LastNameStudent", tbStudLastName.Text);
                        sqlCommand.Parameters.AddWithValue("FirstNameStudent", tbStudName.Text);
                        sqlCommand.Parameters.AddWithValue("MiddleNameStudent", tbStudMiddleName.Text);
                        sqlCommand.Parameters.AddWithValue("DateOfBirthStudent", dpStudDateOfBirth.Text);
                        sqlCommand.Parameters.AddWithValue("NumberPhoneStudent", tbStudNumberPhone.Text);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand = new SqlCommand("Insert Into dbo.[User] " +
                            "(LoginUser, PasswordUser, RoleUser) Values (@LoginUser, @PasswordUser, @RoleUser)", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("LoginUser", tbStudLogin.Text);
                        sqlCommand.Parameters.AddWithValue("PasswordUser", tbStudPassword.Password);
                        sqlCommand.Parameters.AddWithValue("RoleUser", "4");
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

