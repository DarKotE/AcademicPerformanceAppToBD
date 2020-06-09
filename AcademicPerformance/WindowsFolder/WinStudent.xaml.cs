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
    /// Interaction logic for WinStudent.xaml
    /// </summary>
    public partial class WinStudent : Window
    {
        CDataAccess dataAccess = new CDataAccess();
        SqlConnection sqlConnection = new SqlConnection(CSqlHelper.CnnVal("AcademicPerformanceDB"));
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        ClassFolder.CDataGrid classDG;

        public WinStudent()
        {
            InitializeComponent();            
        }

        private void GridRefresh() 
        {
            dgJournal.ItemsSource = dataAccess.GetJournalTableVar().DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridRefresh();
            MessageBox.Show(App.IdUser);
        }

        private void dgJouranl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)dgJournal.SelectedItem;
                tBNumber.Text = dataRowView[0].ToString();
                tbFIOStudent.Text = dataRowView[0].ToString();
                tbNameEvaluation.Text = dataRowView[1].ToString();
                tbEvalustion.Text = dataRowView[2].ToString();
                TbFIOTeacher.Text = dataRowView[3].ToString();
                tbNameDiscipline.Text = dataRowView[4].ToString();
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно желаете выйти?", 
                "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);

            }
        }

        private void miPersonalProfile_Click(object sender, RoutedEventArgs e)
        {
           WinProfileStudent winProfileStudent =
                new WinProfileStudent();
            winProfileStudent.ShowDialog();
        }

        private void tbSerach_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbSerach.Text))
            {
                classDG.LoadDG("Select * from dbo.[Journal] where IdUser='" + App.IdUser + "'");
            }
            else
            {
                classDG.LoadDG($"Select * from dbo.[Journal] where FIOTeacher like '%{tbSerach.Text}%' and IdUser='{App.IdUser}'");
            }
        }
    }
}
