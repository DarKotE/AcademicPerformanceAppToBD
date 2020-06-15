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
    /// Interaction logic for WinProfileStudent.xaml
    /// </summary>
    public partial class WinProfileTeacher : Window
    {
        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
        SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal());
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        public WinProfileTeacher()
        {
            InitializeComponent();
        }       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From dbo.Student " +
                    "Where IdUser='" + App.IdUser + "'", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                tbStudLastName.Text = sqlDataReader[1].ToString();
                tbStudName.Text = sqlDataReader[2].ToString();
                tbStudMiddleName.Text = sqlDataReader[3].ToString();
                dpStudDateOfBirth.Text = sqlDataReader[4].ToString();
                tbStudNumberPhone.Text = sqlDataReader[5].ToString();

                lblStudProfile.Content = lblStudProfile.Content + sqlDataReader[1].ToString() + " "
                    + sqlDataReader[2].ToString() + " " + sqlDataReader[3].ToString();
                sqlDataReader.Close();

                sqlCommand = new SqlCommand("Select * From dbo.[User] Where IdUser='" + App.IdUser + "'", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                tbStudLogin.Text = sqlDataReader[1].ToString();
                tbStudPassword.Text = sqlDataReader[2].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошика", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                sqlConnection.Open();

            sqlCommand = new SqlCommand($"update [Student] " +
                $"set LastNameStudent='{tbStudLastName.Text}'," +
                $"FirstNameStudent='{tbStudName.Text}', " +
                $"MiddleNameStudent='{tbStudMiddleName.Text}'," +
                $"DateOfBirth='{Convert.ToDateTime(dpStudDateOfBirth.Text)}'," +
                $"NumberPhoneStudent='{tbStudNumberPhone.Text}' " +
                $"where [IdUser]='{App.IdUser}'", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlCommand = new SqlCommand($"update [User] " +
                $"set PasswordUser='{tbStudPassword.Text}', " +
                $"LoginUser='{tbStudLogin.Text}' " +
                $"where IdUser='{App.IdUser}'", sqlConnection);
            sqlCommand.ExecuteNonQuery();

            MessageBox.Show("Данные успешно отредактированны!", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
