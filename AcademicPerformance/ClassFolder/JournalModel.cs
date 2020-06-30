using System.ComponentModel;

namespace AcademicPerformance.ClassFolder
{
    public class JournalModel : INotifyPropertyChanged
    {
       
        private int idStudent;
        public int IdStudent
        {
            get { return idStudent; }
            set { idStudent = value; OnPropertyChanged("IdStudent"); }
        }


        private int idTeacher;
        public int IdTeacher
        {
            get { return idTeacher; }
            set { idTeacher = value; OnPropertyChanged("IdTeacher"); }
        }


        private int idDiscipline;
        public int IdDiscipline
        {
            get { return idDiscipline; }
            set { idDiscipline = value; OnPropertyChanged("IdDiscipline"); }
        }


        private int idEvaluation;
        public int IdEvaluation
        {
            get { return idEvaluation; }
            set { idEvaluation = value; OnPropertyChanged("IdEvaluation"); }
        }

        private int idJournal;
        public int IdJournal
        {
            get { return idJournal; }
            set { idJournal = value; OnPropertyChanged("IdJournal"); }
        }


        //Поля отсутствуют внутри таблицы БД и существуют только в модели
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
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));

        }

    }
}
