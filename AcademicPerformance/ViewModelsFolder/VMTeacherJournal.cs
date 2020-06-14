using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AcademicPerformance.ViewModelFolder

{
    public class VMTeacherJournal : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private JournalController teacherJournalController;
        private DisciplineController disciplineController;
        private EvaluationController evaluationController;

        public VMTeacherJournal()
        {

            teacherJournalController = new JournalController();
            disciplineController = new DisciplineController();
            evaluationController = new EvaluationController();
            DisciplineList = new ObservableCollection<DisciplineModel>(disciplineController.GetAll());
            EvaluationList = new ObservableCollection<EvaluationModel>(evaluationController.GetAll());
            SelectedRow = new JournalModel();
            SearchText = "";
            Filter();

            var r = SelectedRow;

        }


        private ObservableCollection<JournalModel> filteredJournalList;

        public ObservableCollection<JournalModel> FilteredJournalList
        {
            get { return filteredJournalList; }
            set { filteredJournalList = value; OnPropertyChanged("FilteredJournalList"); }

        }


        private ObservableCollection<JournalModel> journalList;
        public ObservableCollection<JournalModel> JournalList
        {
            get { return journalList; }
            set { journalList = value; OnPropertyChanged("JournalList"); }

        }


        private ObservableCollection<EvaluationModel> evaluationList;
        public ObservableCollection<EvaluationModel> EvaluationList
        {
            get { return evaluationList; }
            set { evaluationList = value; OnPropertyChanged("EvaluationList"); }

        }

        private ObservableCollection<DisciplineModel> disciplineList;
        public ObservableCollection<DisciplineModel> DisciplineList
        {
            get { return disciplineList; }
            set { disciplineList = value; OnPropertyChanged("DisciplineList"); }

        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                Filter();
                OnPropertyChanged("SearchText");
            }

        }

        private void Filter()
        {
            JournalList = new ObservableCollection<JournalModel>(teacherJournalController.GetAll());
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
            if (FilteredJournalList.Any())
            {
                SelectedRow = FilteredJournalList[0];
            }
        }

        private EvaluationModel selectedNumber;
        public EvaluationModel SelectedNumber
        {
            
            set
            {
                selectedNumber = value;

                if ((EvaluationList != null) && (selectedNumber != null) && (SelectedRow != null))
                    if (SelectedRow.NumberEvaluation != selectedNumber.NumberEvaluation)
                        SelectedRow.NameEvaluation = EvaluationList[5 - selectedNumber.NumberEvaluation].NameEvaluation;
                OnPropertyChanged("SelectedNumber");
            }

        }

        private JournalModel selectedRow;
        public JournalModel SelectedRow
        {
            get
            {
                return selectedRow;
            }
            set 
            {
                selectedRow = value;
                OnPropertyChanged("SelectedRow");

            }

        }

        
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(Message); }
        }


        public void Save(object param)
        {
            MessageBox.Show(Message);
        }

    }
}
