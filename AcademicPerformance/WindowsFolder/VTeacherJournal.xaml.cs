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
using AcademicPerformance.ViewModelFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinTeacher.xaml
    /// </summary>
    public partial class WinTeacher : Window
    {
        

        public WinTeacher()
        {
            InitializeComponent();
            var teacherJournal = new VMTeacherJournal();
            this.DataContext = teacherJournal;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbSearch.Focus();
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
        

        private void MiPersonalProfile_Click(object sender, RoutedEventArgs e)
        {
            WinProfileStudent winProfileStudent =
                 new WinProfileStudent();
            winProfileStudent.ShowDialog();
        }

        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно желаете выйти?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }
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
            //throw new NotImplementedException();

        }

        private void miEditIn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    throw new NotImplementedException();

            //    //ClassFolder.CJournal.IdJournal = classDG.SelectId();
            //    //WinEditIn winEditIn = new WinEditIn();
            //    //winEditIn.ShowDialog();

            //}
            //catch
            //{
            //    MessageBox.Show("ВВыбирите строку в таблице", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
        
    }
}


