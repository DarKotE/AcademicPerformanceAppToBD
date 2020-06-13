using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicPerformance.ClassFolder
{
    public class StudentJournalModel : INotifyPropertyChanged
    {
        private int idJournal;
        public int IdJournal
        {
            get { return idJournal; }
            set { idJournal = value; OnPropertyChanged("IdJournal"); }
        }
        private string fIOStudent;
        public string FIOStudent
        {
            get { return fIOStudent; }
            set { fIOStudent = value; OnPropertyChanged("FIOStudent"); }
        }
        private int numberEvaluation;
        public int NumberEvaluation
        {
            get { return numberEvaluation; }
            set { numberEvaluation = value; OnPropertyChanged("NumberEvaluation"); }
        }
        private string nameEvaluation;
        public string NameEvaluation
        {
            get { return nameEvaluation; }
            set { nameEvaluation = value; OnPropertyChanged("NameEvaluation"); }
        }
        private string fIOTeacher;
        public string FIOTeacher
        {
            get { return fIOTeacher; }
            set { fIOTeacher = value; OnPropertyChanged("FIOTeacher"); }
        }
        private string nameDiscipline;
        public string NameDiscipline
        {
            get { return nameDiscipline; }
            set { nameDiscipline = value; OnPropertyChanged("NameDiscipline"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
