using System.ComponentModel;

namespace AcademicPerformance.ClassFolder
{
    public class EvaluationModel : INotifyPropertyChanged
    {

        private int idEvaluation;
        public int IdEvaluation
        {
            get { return idEvaluation; }
            set { idEvaluation = value; OnPropertyChanged(nameof(IdEvaluation)); }
        }


        private string nameEvaluation;
        public string NameEvaluation
        {
            get { return nameEvaluation; }
            set { nameEvaluation = value; OnPropertyChanged(nameof(NameEvaluation)); }
        }


        private int numberEvaluation;
        public int NumberEvaluation
        {
            get { return numberEvaluation; }
            set { numberEvaluation = value; OnPropertyChanged(nameof(NumberEvaluation)); }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));

        }
    }

}
