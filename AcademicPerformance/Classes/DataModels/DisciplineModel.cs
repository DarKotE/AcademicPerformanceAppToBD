using System.ComponentModel;

namespace AcademicPerformance.Classes.DataModels
{
   public class DisciplineModel : INotifyPropertyChanged
    {
        private int idDiscipline;
        public int IdDiscipline
        {
            get { return idDiscipline; }
            set { idDiscipline = value; OnPropertyChanged(nameof(IdDiscipline)); }
        }

        private string nameDiscipline;
        public string NameDiscipline
        {
            get { return nameDiscipline; }
            set { nameDiscipline = value; OnPropertyChanged(nameof(NameDiscipline)); }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));

        }
    }
}
