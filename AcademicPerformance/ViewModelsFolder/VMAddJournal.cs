using System.Collections.ObjectModel;
using System.Windows;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder
{
    public class VMAddJournal
    {
        private readonly JournalController journalController = new JournalController();
        private readonly TeacherController teacherController = new TeacherController();
        private readonly StudentController studentController = new StudentController();
        private readonly DisciplineController disciplineController = new DisciplineController();
        private readonly EvaluationController evaluationController = new EvaluationController();

        public VMAddJournal()
        {
            SelectedTeacher = new TeacherModel();
            CurrentJournal = new JournalModel();
            StudentList = new ObservableCollection<StudentModel>(studentController.GetAll());
            TeacherList = new ObservableCollection<TeacherModel>(teacherController.GetAll());
            DisciplineList = new ObservableCollection<DisciplineModel>(disciplineController.GetAll());
            EvaluationList = new ObservableCollection<EvaluationModel>(evaluationController.GetAll());
            AddCommand = new RelayCommand(Add);
            if (App.RoleUser == Const.RoleValue.Teacher)
            {
                SelectedTeacher = teacherController.Select(App.IdUser);
            }
        }

        public ObservableCollection<TeacherModel> TeacherList { get; }

        public ObservableCollection<StudentModel> StudentList { get; }

        public RelayCommand AddCommand { get; }

        public JournalModel CurrentJournal { get; set; }

        public TeacherModel CurrentTeacher { get; set; }

        public ObservableCollection<DisciplineModel> DisciplineList { get; }

        public ObservableCollection<EvaluationModel> EvaluationList { get; }

        public string Message { get; set; }

        private DisciplineModel selectedDiscipline;
        public DisciplineModel SelectedDiscipline
        {
            get => selectedDiscipline;
            set
            {
                selectedDiscipline = value;
                if (DisciplineList != null
                    && selectedDiscipline != null
                    && CurrentJournal != null
                    && CurrentJournal.NameDiscipline != selectedDiscipline.NameDiscipline)
                {
                    CurrentJournal.NameDiscipline = selectedDiscipline.NameDiscipline;
                    CurrentJournal.IdDiscipline = selectedDiscipline.IdDiscipline;
                }
            }
        }

        private EvaluationModel selectedEvaluation;
        public EvaluationModel SelectedEvaluation
        {
            get => selectedEvaluation;
            set
            {
                selectedEvaluation = value;
                if (EvaluationList != null
                    && selectedEvaluation != null
                    && CurrentJournal != null
                    && CurrentJournal.NameEvaluation != selectedEvaluation.NameEvaluation)
                {
                    CurrentJournal.NameEvaluation = selectedEvaluation.NameEvaluation;
                    CurrentJournal.IdEvaluation = selectedEvaluation.IdEvaluation;
                }
            }
        }

        private StudentModel selectedStudent;
        public StudentModel SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                if (StudentList != null && selectedStudent != null && CurrentJournal != null
                    && CurrentJournal.FIOStudent != selectedStudent.FullName)
                {
                    CurrentJournal.FIOStudent = selectedStudent.FullName;
                    CurrentJournal.IdStudent = selectedStudent.IdStudent;
                }
            }
        }

        private TeacherModel selectedTeacher;
        public TeacherModel SelectedTeacher
        {
            get => selectedTeacher;
            set
            {
                selectedTeacher = value;
                if (TeacherList != null && selectedTeacher != null && CurrentJournal != null
                    && CurrentJournal.FIOTeacher != selectedTeacher.FullName)
                {
                    CurrentJournal.FIOTeacher = selectedTeacher.FullName;
                    CurrentJournal.IdTeacher = selectedTeacher.IdTeacher;
                } 
            }
        }

        public void Add(object param)
        {
            if (App.RoleUser == Const.RoleValue.Teacher)
                CurrentJournal.IdTeacher = teacherController.Select(App.IdUser)
                    .IdTeacher;
            if (CurrentJournal.IdEvaluation != default &&
                CurrentJournal.IdTeacher != default &&
                CurrentJournal.IdDiscipline != default &&
                CurrentJournal.IdStudent != default)
                Message = journalController.Add(CurrentJournal) ?
                    "Добавлено" : 
                    "При добавлении произошла ошибка";
            else
                Message = "Заполните все поля";
            MessageBox.Show(Message);
        }

    }
}