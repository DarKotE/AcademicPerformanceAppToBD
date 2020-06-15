using System.ComponentModel;
using System.Windows;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder
{

    public class VMAddJournal : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(propertyName));
        }


        private readonly JournalController journalController;
        private readonly TeacherController teacherController;
        public VMAddJournal()
        {

            journalController = new JournalController();
            teacherController = new TeacherController();
            CurrentTeacher = new TeacherModel();
            CurrentTeacher = teacherController.Select(App.IdUser);
            CurrentJournal = new JournalModel();
            addCommand = new RelayCommand(Add);
        }


        private JournalModel currentJournal;
        public JournalModel CurrentJournal
        {
            get { return currentJournal; }
            set
            {
                currentJournal = value;
                OnPropertyChanged("CurrentJournal");
            }
        }
        private TeacherModel currentTeacher;
        public TeacherModel CurrentTeacher
        {
            get { return currentTeacher; }
            set
            {
                currentTeacher = value;
                OnPropertyChanged("CurrentTeacher");
            }
        }


        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get { return addCommand; }
        }


        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }


        public void Add(object param)
        {
            CurrentJournal.IdTeacher = CurrentTeacher.IdTeacher;
            message = journalController.Add(CurrentJournal) ? "Добавлено" 
                : "При добавлении произошла ошибка";
            MessageBox.Show(Message);
        }

    }
}
