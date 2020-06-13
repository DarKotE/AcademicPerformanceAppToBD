using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AcademicPerformance.ClassFolder
{
    public class EvaluationModel : INotifyPropertyChanged
    {

        private int idEvaluation;
        public int IdEvaluation
        {
            get { return idEvaluation; }
            set { idEvaluation = value; OnPropertyChanged("IdEvaluation"); }
        }


        private string nameEvaluation;
        public string NameEvaluation
        {
            get { return nameEvaluation; }
            set { nameEvaluation = value; OnPropertyChanged("NameEvaluation"); }
        }


        private int numberEvaluation;
        public int NumberEvaluation
        {
            get { return numberEvaluation; }
            set { numberEvaluation = value; OnPropertyChanged("NumberEvaluation"); }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }

}
