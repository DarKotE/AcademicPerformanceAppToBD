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
using AcademicPerformance.ClassFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinStudent.xaml
    /// </summary>
    public partial class WinStudent : Window
    {
        private readonly CDataAccess dataAccess = new CDataAccess();
        private readonly DataTable dataTable = new DataTable();

        public WinStudent()
        {            
            InitializeComponent(); 
            dataTable = dataAccess.GetJournalTableVar();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgJournal.ItemsSource = dataTable.DefaultView;
            GridRefresh();
        }


        private void GridRefresh() 
        {
            dgJournal.SelectedItems.Clear();            
            dgJournal.ItemsSource = dataTable.DefaultView;
            if (dgJournal.Items.Count > 0)
            {
                dgJournal.SelectedItem = dgJournal.Items[0];
                PopulateTextBox();
            }
            else
            {
                ClearTextBox();
            };
        }

        private void PopulateTextBox() 
        {
            try
            {
                DataRowView dataRowView = (DataRowView)dgJournal.SelectedItem;
                tBNumber.Text = dataRowView[0].ToString();
                tbFIOStudent.Text = dataRowView[1].ToString();
                tbNameEvaluation.Text = dataRowView[2].ToString();
                tbEvalustion.Text = dataRowView[3].ToString();
                TbFIOTeacher.Text = dataRowView[4].ToString();
                tbNameDiscipline.Text = dataRowView[5].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearTextBox()
        {
            try
            {
                tBNumber.Text = "";
                tbFIOStudent.Text = "";
                tbNameEvaluation.Text = "";
                tbEvalustion.Text = "";
                TbFIOTeacher.Text = "";
                tbNameDiscipline.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void FilterGrid(string textSearch)
        {
            try
            {
                dataTable.DefaultView.RowFilter = string.Format(
                    "NameEvaluation LIKE '%{0}%'"
                    + "OR FIOTeacher LIKE '%{0}%'"
                    + "OR FIOStudent LIKE '%{0}%'"
                    + "OR NameDiscipline LIKE '%{0}%'", textSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgJouranl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PopulateTextBox();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно желаете выйти?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterGrid(tbSearch.Text);
            GridRefresh();
        }

        private void MiPersonalProfile_Click(object sender, RoutedEventArgs e)
        {
            WinProfileStudent winProfileStudent =
                 new WinProfileStudent();
            winProfileStudent.ShowDialog();
        }

        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
