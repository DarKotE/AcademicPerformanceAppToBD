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

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinTeacher.xaml
    /// </summary>
    public partial class WinTeacher : Window
    {
        private readonly CDataAccess dataAccess = new CDataAccess();
        private readonly DataTable dataTable = new DataTable();

        public WinTeacher()
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
                //cbNumber.Text = dataRowView[0].ToString();
                cbFIOStudent.Text = dataRowView[1].ToString();
                cbNameEvaluation.Text = dataRowView[2].ToString();
                cbEvaluation.Text = dataRowView[3].ToString();
                tbFIOTeacher.Text = dataRowView[4].ToString();
                cbNameDiscipline.Text = dataRowView[5].ToString();
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
                //tBNumber.Text = "";
                cbFIOStudent.Text = "";
                cbNameEvaluation.Text = "";
                cbEvaluation.Text = "";
                tbFIOTeacher.Text = "";
                cbNameDiscipline.Text = "";
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

        //private void miSaveChanges_Click(object sender, RoutedEventArgs e)
        //{

        //    try
        //    {
        //        ClassFolder.ClassDiscipline.IdDiscipline = Convert.ToInt32(cbNameDiscipline.SelectedValue.ToString());
        //        ClassFolder.ClassEvaluation.IdEvaluation = Convert.ToInt32(cbEvalustion.SelectedValue.ToString());
        //        IdStunet();
        //        IdTeacher();
        //        sqlConnection.Open();
        //        sqlCommand = new SqlCommand($"update [Journal]" +
        //            $"set IdStudent='{ClassFolder.ClassStudent.IdStudent}'," +
        //            $"IdTeacher='{ClassFolder.ClassTeacher.IdTeacher}'," +
        //            $"IdDiscipline='{ClassFolder.ClassDiscipline.IdDiscipline}'," +
        //            $"IdEvaluation='{ClassFolder.ClassEvaluation.IdEvaluation}',", sqlConnection);
        //        sqlCommand.ExecuteNonQuery();

        //        MessageBox.Show("Изменения успешно сохранены", "Информация", MessageBoxButton.OK,
        //            MessageBoxImage.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        private void miAdd_Click(object sender, RoutedEventArgs e)
        {
            //WinAdd winAdd = new WinAdd();
            //winAdd.ShowDialog();
            //classDG.LoadDG("Select * from dbo.ViewJournal" +
            //    $"where IdUserTeahcer={App.IdUser}");
            throw new NotImplementedException();

        }

        private void miEditIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                throw new NotImplementedException();

                //ClassFolder.CJournal.IdJournal = classDG.SelectId();
                //WinEditIn winEditIn = new WinEditIn();
                //winEditIn.ShowDialog();

            }
            catch
            {
                MessageBox.Show("ВВыбирите строку в таблице", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbNameEvaluation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
            //ClassFolder.ClassEvaluation.IdEvaluation = Convert.ToInt32(cbNameEvaluation.SelectedValue.ToString());

            //try
            //{
            //    sqlConnection.Open();
            //    sqlCommand = new SqlCommand("Select NumberEvaluation from dbo.Evaluation" +
            //        "where IdEvaluation='" + ClassFolder.ClassEvaluation.IdEvaluation + "'", sqlConnection);

            //    sqlDataReader = sqlCommand.ExecuteReader();
            //    sqlDataReader.Read();

            //    cbEvalustion.Text = sqlDataReader[0].ToString();
            //    sqlDataReader.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //finally
            //{
            //    sqlConnection.Close();
        }


    }
}


