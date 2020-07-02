using System.ComponentModel;

namespace AcademicPerformance.Classes.DataModels
{
    public class JournalModel : INotifyPropertyChanged
    {
       
        private int idStudent;
        public int IdStudent
        {
            get { return idStudent; }
            set { idStudent = value; OnPropertyChanged(nameof(IdStudent)); }
        }


        private int idTeacher;
        public int IdTeacher
        {
            get { return idTeacher; }
            set { idTeacher = value; OnPropertyChanged(nameof(IdTeacher)); }
        }


        private int idDiscipline;
        public int IdDiscipline
        {
            get { return idDiscipline; }
            set { idDiscipline = value; OnPropertyChanged(nameof(IdDiscipline)); }
        }


        private int idEvaluation;
        public int IdEvaluation
        {
            get { return idEvaluation; }
            set { idEvaluation = value; OnPropertyChanged(nameof(IdEvaluation)); }
        }

        private int idJournal;
        public int IdJournal
        {
            get { return idJournal; }
            set { idJournal = value; OnPropertyChanged(nameof(IdJournal)); }
        }


        //Поля отсутствуют внутри таблицы БД и существуют только в модели
        private string fIOStudent;
        public string FIOStudent
        {
            get { return fIOStudent; }
            set { fIOStudent = value; OnPropertyChanged(nameof(FIOStudent)); }
        }


        private int numberEvaluation;
        public int NumberEvaluation
        {
            get { return numberEvaluation; }
            set { numberEvaluation = value; OnPropertyChanged(nameof(NumberEvaluation)); }
        }


        private string nameEvaluation;
        public string NameEvaluation
        {
            get { return nameEvaluation; }
            set { nameEvaluation = value; OnPropertyChanged(nameof(NameEvaluation)); }
        }


        private string fIOTeacher;
        public string FIOTeacher
        {
            get { return fIOTeacher; }
            set { fIOTeacher = value; OnPropertyChanged(nameof(FIOTeacher)); }
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
