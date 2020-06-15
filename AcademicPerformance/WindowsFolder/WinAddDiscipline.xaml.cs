using System;
using System.Collections.Generic;
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
    }
}
