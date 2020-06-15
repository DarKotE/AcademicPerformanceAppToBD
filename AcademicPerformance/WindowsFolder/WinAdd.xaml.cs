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
using AcademicPerformance.ViewModelFolder;
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinAdd.xaml
    /// </summary>
    public partial class WinAdd : Window
    {
     
        public WinAdd()
        {
            InitializeComponent();
            var addJournal = new VMAddJournal();
            this.DataContext = addJournal;
        }
        private void LoadIdTeacher()
        {
            
            //try
            //{
            //    sqlConnection.Open();
                
            //    sqlCommand = new SqlCommand("Select IdTeacher from dbo.Teacher Where IdUser='" + 5/*App.IdUser*/ + "'", sqlConnection);

            //    sqlDataReader = sqlCommand.ExecuteReader();
            //    sqlDataReader.Read();

            //    tbIdTeacher.Text = sqlDataReader[0].ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
            //}
            //finally
            //{
            //    sqlConnection.Close();
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadIdTeacher();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    sqlConnection.Open();
            //    sqlCommand = new SqlCommand("Insert Into dbo.[Journal] (IdStudent,IdTeacher,IdDiscipline,IdEvaluation)" +
            //        "Values (@IdStudent,@IdTeacher,@IdDiscipline,@IdEvaluation)", sqlConnection);

            //    sqlCommand.Parameters.AddWithValue("IdStudent", tbIdStudent.Text);
            //    sqlCommand.Parameters.AddWithValue("IdTeacher", tbIdTeacher.Text);
            //    sqlCommand.Parameters.AddWithValue("IdDiscipline", tbIdDiscipline.Text);
            //    sqlCommand.Parameters.AddWithValue("IdEvaluation", tbIdEvaluation.Text);

            //    sqlCommand.ExecuteNonQuery();
            //    MessageBox.Show("Успешно добавлено", "Информация",
            //        MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //finally
            //{
            //    sqlConnection.Close();
            //}
        }

    }
}
