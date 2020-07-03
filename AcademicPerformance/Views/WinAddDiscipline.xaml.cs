using System.Windows;
using AcademicPerformance.Classes.DataAdapters;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.Views
{
    /// <summary>
    /// Логика взаимодействия для WinAddDiscipline.xaml
    /// </summary>
    public partial class WinAddDiscipline
    {
        private readonly DisciplineAdapter disciplineAdapter = new DisciplineAdapter();

        public WinAddDiscipline()
        {
            InitializeComponent();
        }
        
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {

            var newDiscipline = new DisciplineModel {NameDiscipline = tbNameDiscipline.Text};
            MessageBox.Show(disciplineAdapter.AddDiscipline(newDiscipline) ? "Добавлено" : "Не добавлено");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}