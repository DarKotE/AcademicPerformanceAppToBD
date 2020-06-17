using System;
using System.Windows;
using System.Data.SqlClient;
using AcademicPerformance.ClassFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для WinAddDiscipline.xaml
    /// </summary>
    public partial class WinAddDiscipline : Window
    {
        SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal());
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;


        public WinAddDiscipline()
        {
            InitializeComponent();
        }



        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert Into dbo.[Discipline] (NameDiscipline)" +
                    "Values (@NameDiscipline)", sqlConnection);

                
                sqlCommand.Parameters.AddWithValue("NameDiscipline", tbNameDiscipline.Text);
                

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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
