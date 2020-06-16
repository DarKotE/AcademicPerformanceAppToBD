using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder

{
    public class VMAdminJournal : INotifyPropertyChanged
    {
        public VMAdminJournal()
        {
            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
            teacherJournalController = new JournalController();
            disciplineController = new DisciplineController();
            evaluationController = new EvaluationController();
            LoadData();
            Filter();
        }

        private void LoadData()
        {
            DisciplineList = new ObservableCollection<DisciplineModel>(disciplineController.GetAll());
            EvaluationList = new ObservableCollection<EvaluationModel>(evaluationController.GetAll());
            JournalList = new ObservableCollection<JournalModel>(teacherJournalController.GetAllFull());
            SelectedRow = new JournalModel();
            SearchText = "";
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly JournalController teacherJournalController;
        private readonly DisciplineController disciplineController;
        private readonly EvaluationController evaluationController;


        private ObservableCollection<JournalModel> filteredJournalList;

        public ObservableCollection<JournalModel> FilteredJournalList
        {
            get => filteredJournalList;
            set
            {
                filteredJournalList = value;
                OnPropertyChanged("FilteredJournalList");
            }
        }


        private ObservableCollection<JournalModel> journalList;

        public ObservableCollection<JournalModel> JournalList
        {
            get => journalList;
            set
            {
                journalList = value;
                OnPropertyChanged("JournalList");
            }
        }


        public ObservableCollection<EvaluationModel> EvaluationList { get; set; }

        public ObservableCollection<DisciplineModel> DisciplineList { get; set; }

        private string searchText;

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Filter();
                OnPropertyChanged("SearchText");
            }
        }

        private EvaluationModel selectedNumber;

        public EvaluationModel SelectedNumber
        {
            set
            {
                selectedNumber = value;
                if (EvaluationList != null && selectedNumber != null && SelectedRow != null
                    && SelectedRow.NumberEvaluation != selectedNumber.NumberEvaluation)
                {
                    SelectedRow.NameEvaluation = selectedNumber.NameEvaluation;
                    SelectedRow.IdEvaluation = selectedNumber.IdEvaluation;
                }
            }
        }

        private DisciplineModel selectedDiscipline;

        public DisciplineModel SelectedDiscipline
        {
            set
            {
                selectedDiscipline = value;
                if (DisciplineList != null && selectedDiscipline != null && SelectedRow != null
                    && SelectedRow.NameDiscipline != selectedDiscipline.NameDiscipline)
                {
                    SelectedRow.NameDiscipline = selectedDiscipline.NameDiscipline;
                    SelectedRow.IdDiscipline = selectedDiscipline.IdDiscipline;
                }
            }
        }


        private JournalModel selectedRow;

        public JournalModel SelectedRow
        {
            get => selectedRow;
            set
            {
                selectedRow = value;
                OnPropertyChanged("SelectedRow");
            }
        }


        private void Filter()
        {
            FilteredJournalList =
                new ObservableCollection<JournalModel>(
                    from item
                        in JournalList
                    where item.NameEvaluation.ToUpper().Contains(SearchText.ToUpper())
                          || item.FIOTeacher.ToUpper().Contains(SearchText.ToUpper())
                          || item.FIOStudent.ToUpper().Contains(SearchText.ToUpper())
                          || item.NameDiscipline.ToUpper().Contains(SearchText.ToUpper())
                          || item.NumberEvaluation.ToString().ToUpper().Contains(SearchText.ToUpper())
                          || item.IdJournal.ToString().ToUpper().Contains(SearchText.ToUpper())
                    select item);
            if (FilteredJournalList.Any()) SelectedRow = FilteredJournalList[0];
        }


        public RelayCommand SaveCommand { get; }


        public RelayCommand DeleteCommand { get; }

        public string Message { get; set; }


        public void Save(object param)
        {
            var isAllSaved = true;
            foreach (var item in filteredJournalList)
                if (!teacherJournalController.Update(item))
                    isAllSaved = false;

            Message = isAllSaved ? "Изменения сохранены" : "При сохранении произошла ошибка";
            MessageBox.Show(Message);
            LoadData();
        }

        public void Delete(object param)
        {
            var isDeleted = teacherJournalController.Delete(SelectedRow.IdJournal);
            Message = isDeleted ? "Удалено" : "При удалении произошла ошибка";
            MessageBox.Show(Message);
            LoadData();
        }
    }
}