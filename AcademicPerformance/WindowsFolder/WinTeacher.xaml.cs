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
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        SqlDataAdapter dataAdapter;
        DataSet dataSet;
        ClassFolder.CDataGrid classDG;
        public WinTeacher()
        {
            InitializeComponent();
            classDG = new ClassFolder.CDataGrid(dgJournal);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            classDG.LoadDG("select * from dbo.[ViewJournal]" +
                $"where IdUserTeacher={App.IdUser}");
            LoadFIOStudent();
            LoadNameEvaluation();
            LoadEvaluation();
            LoadDiscipline();
            LoadFIOTeacher();
        }

        private void dgJouranl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string id = classDG.SelectId();
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * from dbo.ViewJournal Where IdJournal='" + id + "'", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();

                cbFIOStuent.Text = sqlDataReader[1].ToString();
                cbNameEvaluation.Text = sqlDataReader[2].ToString();
                cbEvalustion.Text = sqlDataReader[3].ToString();
                tbFIOTeacher.Text = sqlDataReader[4].ToString();
                cbNameDiscipline.Text = sqlDataReader[5].ToString();
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
        private void LoadFIOStudent()
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("select * from dbo.Student order by IdStudent ASC", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    cbFIOStuent.Items.Add(sqlDataReader[2].ToString() + " " + sqlDataReader[3].ToString() + " " + sqlDataReader[4].ToString());
                }
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
        private void LoadNameEvaluation()
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter("Select IdEvaluation,NameEvaluation from dbo.Evaluation order by IdEvaluation",
                    sqlConnection);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Evaluation");
                cbNameEvaluation.ItemsSource = dataSet.Tables["Evaluation"].DefaultView;
                cbNameEvaluation.DisplayMemberPath = dataSet.Tables["Evaluation"].Columns["NameEvaluation"].ToString();
                cbNameEvaluation.SelectedValuePath = dataSet.Tables["Evaluation"].Columns["IdEvaluation"].ToString();
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
        private void LoadEvaluation()
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter("select IdEvaluation,NumberEvaluation from dbo.Evaluation order by IdEvaluation",
                    sqlConnection);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Evaluation");
                cbEvalustion.ItemsSource = dataSet.Tables["Evaluation"].DefaultView;
                cbEvalustion.DisplayMemberPath = dataSet.Tables["Evaluation"].Columns["NumberEvaluation"].ToString();
                cbEvalustion.SelectedValuePath = dataSet.Tables["Evaluation"].Columns["IdEvaluation"].ToString();
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
        private void LoadFIOTeacher()
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter("select * from dbo.Teacher order by IdTeacher ASC", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

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
        private void LoadDiscipline()
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter("select IdDiscipline,NameDiscipline from dbo.Discipline order by IdDiscipline",
                    sqlConnection);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Discipline");
                cbNameDiscipline.ItemsSource = dataSet.Tables["Discipline"].DefaultView;
                cbNameDiscipline.DisplayMemberPath = dataSet.Tables["Discipline"].Columns["NameDiscipline"].ToString();
                cbNameDiscipline.SelectedValuePath = dataSet.Tables["Discipline"].Columns["IdDiscipline"].ToString();
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

        //private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(tbSearch.Text))
        //    {
        //        classDG.LoadDG("Select * from dbo.ViewJournal where IdUser'" + App.IdUser + "'");
        //    }
        //    else
        //    {
        //        classDG.LoadDG($"select * from dbo.Viewjournal where FIOStudent like '%{tbSearch.Text}%' and IdUser='{App.IdUser}'");
        //    }
        //}
        private void IdTeacher()
        {
            try
            {
                string[] fioteacher = tbFIOTeacher.Text.Split(new char[] { ' ' });
                string lName = fioteacher[0];
                string fName = fioteacher[1];
                string mName = fioteacher[2];

                sqlCommand = new SqlCommand(@"Select IdTeacher from dbo.Teacher where LastNameTeacher='" + lName + "'" +
                    "and FirstNameTeacher='" + fName + "' and MiddleNameTeacher='" + mName + "'", sqlConnection);
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                ClassFolder.CTeacher.IdTeacher = Convert.ToInt32(sqlDataReader[0].ToString());
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
        private void IdStunet()
        {
            try
            {
                string[] fiostudent = cbFIOStuent.Text.Split(new char[] { ' ' });
                string lName = fiostudent[0];
                string fName = fiostudent[1];
                string mName = fiostudent[2];
                sqlCommand = new SqlCommand(@"Select IdStudent from dbo.Student where LastNameStudent='" + lName + "'" +
                    "and FirstNameStudent='" + fName + "' and MiddleNameStudent='" + mName + "'", sqlConnection);
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                ClassFolder.CStudent.IdStudent = Convert.ToInt32(sqlDataReader[0].ToString());
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

        private void cbNameEvaluation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            //}
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
            WinAdd winAdd = new WinAdd();
            winAdd.ShowDialog();
            classDG.LoadDG("Select * from dbo.ViewJournal" +
                $"where IdUserTeahcer={App.IdUser}");
        }

        private void miEditIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClassFolder.CJournal.IdJournal = classDG.SelectId();
                WinEditIn winEditIn = new WinEditIn();
                winEditIn.ShowDialog();

            }
            catch
            {
                MessageBox.Show("ВВыбирите строку в таблице", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
    }
}



