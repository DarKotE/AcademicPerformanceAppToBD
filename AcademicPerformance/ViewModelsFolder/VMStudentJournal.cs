using System.Linq;
using System.ComponentModel;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;
using System.Collections.ObjectModel;
using System.Windows;

namespace AcademicPerformance.ViewModelsFolder

{
    public class VMStudentJournal : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private JournalController studentJournalController;

        public VMStudentJournal()
        {
            studentJournalController = new JournalController();
            LoadData();
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

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set { 
                searchText = value; 
                OnPropertyChanged("SearchText");
                Filter();
            }

        }

        private void Filter()
        {
                JournalList = new ObservableCollection<JournalModel>(studentJournalController.GetAll());
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


        private JournalModel selectedRow;
        public JournalModel SelectedRow
        {
            get { return selectedRow; }
            set { selectedRow = value; OnPropertyChanged("SelectedRow"); }

        }


        private void LoadData()
        {
            SearchText="";
            Filter();

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
