using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder

{
    public class VMJournal : INotifyPropertyChanged
    {
        private string message;
        private string searchText;
        private DisciplineModel selectedDiscipline;
        private EvaluationModel selectedNumber;
        private JournalModel selectedRow;

        public VMJournal()
        {
            switch (App.RoleUser)
            {
                case 5:
                    SaveCommand = new RelayCommand(Save);
                    DeleteCommand = new RelayCommand(Delete);
                    JournalController = new JournalController();
                    DisciplineController = new DisciplineController();
                    EvaluationController = new EvaluationController();
                    LoadData();
                    break;
                case 4:
                    JournalController = new JournalController();
                    LoadData();
                    break;
            }
        }

        public DisciplineController DisciplineController { get; }
        public EvaluationController EvaluationController { get; }
        public JournalController JournalController { get; }

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

        private ObservableCollection<EvaluationModel> evaluationList;

        public ObservableCollection<EvaluationModel> EvaluationList
        {
            get => evaluationList;
            set
            {
                evaluationList = value;
                OnPropertyChanged("EvaluationList");
            }
        }

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

        public JournalModel SelectedRow
        {
            get => selectedRow;
            set
            {
                selectedRow = value;
                OnPropertyChanged("SelectedRow");
            }
        }


        public RelayCommand SaveCommand { get; }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged(Message);
            }
        }


        public ObservableCollection<DisciplineModel> DisciplineList { get; set; }

        public EvaluationModel SelectedNumber
        {
            set
            {
                selectedNumber = value;
                if (EvaluationList == null || selectedNumber == null || SelectedRow == null ||
                    SelectedRow.NumberEvaluation == selectedNumber.NumberEvaluation) return;
                SelectedRow.NameEvaluation = selectedNumber.NameEvaluation;
                SelectedRow.IdEvaluation = selectedNumber.IdEvaluation;
            }
        }

        public DisciplineModel SelectedDiscipline
        {
            set
            {
                selectedDiscipline = value;
                if (DisciplineList == null || selectedDiscipline == null || SelectedRow == null ||
                    SelectedRow.NameDiscipline == selectedDiscipline.NameDiscipline) return;
                SelectedRow.NameDiscipline = selectedDiscipline.NameDiscipline;
                SelectedRow.IdDiscipline = selectedDiscipline.IdDiscipline;
            }
        }

        public RelayCommand DeleteCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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


        private void LoadData()
        {
            switch (App.RoleUser)
            {
                case 5:
                    DisciplineList = new ObservableCollection<DisciplineModel>(DisciplineController.GetAll());
                    EvaluationList = new ObservableCollection<EvaluationModel>(EvaluationController.GetAll());
                    JournalList = new ObservableCollection<JournalModel>(JournalController.GetAll());
                    SelectedRow = new JournalModel();
                    SearchText = "";
                    JournalList = new ObservableCollection<JournalModel>(JournalController.GetAll());
                    SearchText = "";
                    Filter();
                    break;
                case 4:
                    JournalList = new ObservableCollection<JournalModel>(JournalController.GetAll());
                    SelectedRow = new JournalModel();
                    SearchText = "";
                    break;
            }
        }


        public void Save(object param)
        {
            var isAllSaved = true;
            foreach (var item in filteredJournalList)
                if (!JournalController.Update(item))
                    isAllSaved = false;

            Message = isAllSaved ? "Изменения сохранены" : "При сохранении произошла ошибка";
            MessageBox.Show(Message);
            LoadData();
        }

        public void Delete(object param)
        {
            var isDeleted = JournalController.Delete(SelectedRow.IdJournal);
            Message = isDeleted ? "Удалено" : "При удалении произошла ошибка";
            MessageBox.Show(Message);
            LoadData();
        }
    }
}