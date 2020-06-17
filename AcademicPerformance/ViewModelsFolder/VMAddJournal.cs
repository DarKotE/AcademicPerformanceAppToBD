using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder
{
    public class VMAddJournal : INotifyPropertyChanged
    {
        private readonly JournalController journalController;
        private readonly TeacherController teacherController;
        private readonly ObservableCollection<StudentModel> studentList;
        private readonly ObservableCollection<TeacherModel> teacherList;
        private TeacherModel currentTeacher;
        private string message;
        private DisciplineModel selectedDiscipline;
        private EvaluationModel selectedEvaluation;
        private StudentModel selectedStudent;
        private TeacherModel selectedTeacher;

        public VMAddJournal()
        {
            SelectedTeacher = new TeacherModel();
            var disciplineController = new DisciplineController();
            var evaluationController = new EvaluationController();
            journalController = new JournalController();
            teacherController = new TeacherController();
            var studentController = new StudentController();
            studentList = new ObservableCollection<StudentModel>(studentController.GetAll());
            teacherList = new ObservableCollection<TeacherModel>(teacherController.GetAll());
            DisciplineList = new ObservableCollection<DisciplineModel>(disciplineController.GetAll());
            EvaluationList = new ObservableCollection<EvaluationModel>(evaluationController.GetAll());
            CurrentJournal = new JournalModel();
            if (App.RoleUser == 5) SelectedTeacher = teacherController.Select(App.IdUser);

            AddCommand = new RelayCommand(Add);
        }

        public RelayCommand AddCommand { get; }
        private JournalModel currentJournal;
        public JournalModel CurrentJournal
        {
            get => currentJournal;
            set
            {
                currentJournal = value;
                OnPropertyChanged("CurrentJournal");
            }
        }

        public TeacherModel CurrentTeacher
        {
            get => currentTeacher;
            set
            {
                currentTeacher = value;
                OnPropertyChanged("CurrentTeacher");
            }
        }

        public ObservableCollection<DisciplineModel> DisciplineList { get; set; }

        public ObservableCollection<EvaluationModel> EvaluationList { get; set; }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public DisciplineModel SelectedDiscipline
        {
            get => selectedDiscipline;
            set
            {
                selectedDiscipline = value;
                if (DisciplineList != null && selectedDiscipline != null && CurrentJournal != null
                    && CurrentJournal.NameDiscipline != selectedDiscipline.NameDiscipline)
                {
                    CurrentJournal.NameDiscipline = selectedDiscipline.NameDiscipline;
                    CurrentJournal.IdDiscipline = selectedDiscipline.IdDiscipline;
                }

                OnPropertyChanged("SelectedDiscipline");
            }
        }

        public EvaluationModel SelectedEvaluation
        {
            get => selectedEvaluation;
            set
            {
                selectedEvaluation = value;
                if (EvaluationList != null && selectedEvaluation != null && CurrentJournal != null
                    && CurrentJournal.NameEvaluation != selectedEvaluation.NameEvaluation)
                {
                    CurrentJournal.NameEvaluation = selectedEvaluation.NameEvaluation;
                    CurrentJournal.IdEvaluation = selectedEvaluation.IdEvaluation;
                }

                OnPropertyChanged("SelectedEvaluation");
            }
        }

        public StudentModel SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                if (studentList != null && selectedStudent != null && CurrentJournal != null
                    && CurrentJournal.FIOStudent != selectedStudent.FullName)
                {
                    CurrentJournal.FIOStudent = selectedStudent.FullName;
                    CurrentJournal.IdStudent = selectedStudent.IdStudent;
                }

                OnPropertyChanged("SelectedStudent");
            }
        }

        public TeacherModel SelectedTeacher
        {
            get => selectedTeacher;
            set
            {
                selectedTeacher = value;
                if (teacherList != null && selectedTeacher != null && CurrentJournal != null
                    && CurrentJournal.FIOTeacher != selectedTeacher.FullName)
                {
                    CurrentJournal.FIOTeacher = selectedTeacher.FullName;
                    CurrentJournal.IdTeacher = selectedTeacher.IdTeacher;
                }

                OnPropertyChanged("SelectedTeacher");
            }
        }




        

        public event PropertyChangedEventHandler PropertyChanged;


        public void Add(object param)
        {
            if (App.RoleUser == 5)
                CurrentJournal.IdTeacher = teacherController.Select(App.IdUser)
                    .IdTeacher;
            if (CurrentJournal.IdEvaluation != default &&
                CurrentJournal.IdTeacher != default &&
                CurrentJournal.IdDiscipline != default &&
                CurrentJournal.IdStudent != default)
                message = journalController.Add(CurrentJournal) ? "Добавлено" : "При добавлении произошла ошибка";
            else
                message = "Заполните все поля";
            MessageBox.Show(Message);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}