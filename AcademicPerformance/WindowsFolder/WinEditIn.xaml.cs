using System.Data.SqlClient;
using System.Windows;
using AcademicPerformance.ClassFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinEditIn.xaml
    /// </summary>
    public partial class WinEditIn : Window
    {
        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
        SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal());
        private SqlCommand sqlCommand;
        private SqlDataReader sqlDataReader;
        private SqlDataAdapter dataAdapter;



        public WinEditIn()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        //    try
        //    {
        //        sqlConnection.Open();
        //        sqlCommand = new SqlCommand("Select * from dbo.Journal " +
        //            $"where IdJournal='{ClassFolder.CJournal.IdJournal}'",sqlConnection);
        //        sqlDataReader = sqlCommand.ExecuteReader();
        //        sqlDataReader.Read();

        //        tbIdStudent.Text = sqlDataReader[1].ToString();
        //        tbIdEvaluation.Text = sqlDataReader[4].ToString();
        //        tbIdTeacher.Text = sqlDataReader[2].ToString();
        //        tbIdDiscipline.Text = sqlDataReader[3].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка",
        //            MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
        //    try
        //    {
        //        sqlConnection.Open();
        //        sqlCommand = new SqlCommand($"update dbo.Journal set IdStudent='{tbIdStudent.Text}'," +
        //            $"IdTeacher='{tbIdTeacher.Text}'," +
        //            $"IdDiscipline='{tbIdDiscipline.Text}'," +
        //            $"IdEvaluation='{tbIdEvaluation.Text}'" +
        //            $"where IdJournal='{ClassFolder.CJournal.IdJournal}'", sqlConnection);

        //        sqlCommand.ExecuteNonQuery();
        //        MessageBox.Show("Данные успешно отредактированы", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
         //}
        }
    }
}
