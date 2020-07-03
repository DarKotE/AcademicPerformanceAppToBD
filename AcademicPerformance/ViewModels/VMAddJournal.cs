using System.Collections.ObjectModel;
using System.Windows;
using AcademicPerformance.Classes;
using AcademicPerformance.Classes.DataAdapters;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.ViewModels
{
    public class VMAddJournal
    {
        private readonly JournalAdapter journalAdapter = new JournalAdapter();
        private readonly TeacherAdapter teacherAdapter = new TeacherAdapter();
        private readonly StudentAdapter studentAdapter = new StudentAdapter();
        private readonly DisciplineAdapter disciplineAdapter = new DisciplineAdapter();
        private readonly EvaluationAdapter evaluationAdapter = new EvaluationAdapter();

        public VMAddJournal()
        {
            SelectedTeacher = new TeacherModel();
            CurrentJournal = new JournalModel();
            StudentList = new ObservableCollection<StudentModel>(studentAdapter.GetAllUser());
            TeacherList = new ObservableCollection<TeacherModel>(teacherAdapter.GetAllTeacher());
            DisciplineList = new ObservableCollection<DisciplineModel>(disciplineAdapter.GetAll());
            EvaluationList = new ObservableCollection<EvaluationModel>(evaluationAdapter.GetAllEvaluation());
            AddCommand = new RelayCommand(Add);
            if (App.RoleUser == Const.RoleValue.Teacher)
            {
                SelectedTeacher = teacherAdapter.GetTeacherById(App.IdUser);
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
                CurrentJournal.IdTeacher = teacherAdapter.GetTeacherById(App.IdUser)
                    .IdTeacher;
            if (CurrentJournal.IdEvaluation != default &&
                CurrentJournal.IdTeacher != default &&
                CurrentJournal.IdDiscipline != default &&
                CurrentJournal.IdStudent != default)
                Message = journalAdapter.AddJournal(CurrentJournal) ?
                    "Добавлено" : 
                    "При добавлении произошла ошибка";
            else
                Message = "Заполните все поля";
            MessageBox.Show(Message);
        }

    }
}