using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder
{

    public class VMAddJournal : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private JournalController journalController;
        public VMAddJournal()
        {

            journalController = new JournalController();
            // LoadData();
            //CurrentUser = new UserModel();
            addCommand = new RelayCommand(Add);
        }
        

        private UserModel currentUser;
        public UserModel CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged("CurrentUser");
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
            set { message = value; OnPropertyChanged(Message); }
        }


        public void Add(object param)
        {


        }
    }
}
