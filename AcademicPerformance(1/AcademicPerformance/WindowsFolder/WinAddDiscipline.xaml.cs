using System;
using System.Windows;
using System.Data.SqlClient;
using AcademicPerformance.ClassFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для WinAddDiscipline.xaml
    /// </summary>
    public partial class WinAddDiscipline
    {
        private readonly DisciplineController disciplineController = new DisciplineController();

        public WinAddDiscipline()
        {
            InitializeComponent();
        }
        
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {

            var newDiscipline = new  DisciplineModel();
            newDiscipline.NameDiscipline=tbNameDiscipline.Text;
            MessageBox.Show(disciplineController.Add(newDiscipline) ? "Добавлено" : "Не добавлено");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}