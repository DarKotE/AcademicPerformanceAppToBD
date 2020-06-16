using System.Collections.ObjectModel;
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
            SelectedTeacher = new TeacherModel();
            var disciplineController = new DisciplineController();
            var evaluationController = new EvaluationController();
            journalController = new JournalController();
            teacherController = new TeacherController();
            var studentController = new StudentController();
            StudentList = new ObservableCollection<StudentModel>(studentController.GetAll());
            TeacherList = new ObservableCollection<TeacherModel>(teacherController.GetAll());
            DisciplineList = new ObservableCollection<DisciplineModel>(disciplineController.GetAll());
            EvaluationList = new ObservableCollection<EvaluationModel>(evaluationController.GetAll());
            CurrentJournal = new JournalModel();
            if (App.RoleUser == 5)
            {
                SelectedTeacher = teacherController.Select(App.IdUser);
            }
            
            addCommand = new RelayCommand(Add);
        }

        public ObservableCollection<StudentModel> StudentList { get; set; }

        public ObservableCollection<TeacherModel> TeacherList { get; set; }

        public ObservableCollection<EvaluationModel> EvaluationList { get; set; }

        public ObservableCollection<DisciplineModel> DisciplineList { get; set; }

        private DisciplineModel selectedDiscipline;
        public DisciplineModel SelectedDiscipline
        {
            get { return selectedDiscipline;}
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
        private EvaluationModel selectedEvaluation;
        public EvaluationModel SelectedEvaluation
        {
            get { return selectedEvaluation; }
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

        private TeacherModel selectedTeacher;
        public TeacherModel SelectedTeacher
        {
            get { return selectedTeacher; }
            set
            {
                selectedTeacher = value;
                if (TeacherList != null && selectedTeacher != null && CurrentJournal != null
                    && CurrentJournal.FIOTeacher != selectedTeacher.FullName)
                {
                    CurrentJournal.FIOTeacher = selectedTeacher.FullName;
                    CurrentJournal.IdTeacher = selectedTeacher.IdTeacher;
                }
                OnPropertyChanged("SelectedTeacher");
            }

        }

        private StudentModel selectedStudent;
        public StudentModel SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                if (StudentList != null && selectedStudent != null && CurrentJournal != null
                    && CurrentJournal.FIOStudent != selectedStudent.FullName)
                {
                    CurrentJournal.FIOStudent = selectedStudent.FullName;
                    CurrentJournal.IdStudent = selectedStudent.IdStudent;
                }
                OnPropertyChanged("SelectedStudent");
            }

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

            if (App.RoleUser == 5)
            {
                CurrentJournal.IdTeacher = teacherController.Select(App.IdUser).IdTeacher;
            }
            if ((CurrentJournal.IdEvaluation!=default)&&(CurrentJournal.IdTeacher != default) 
                                                      && (CurrentJournal.IdDiscipline != default)&& (CurrentJournal.IdStudent != default))
            {
                message = journalController.Add(CurrentJournal) ? "Добавлено" 
                : "При добавлении произошла ошибка";

            }
            else
            {
                message="Заполните все поля";
            }
            MessageBox.Show(Message);
        }

    }
}
