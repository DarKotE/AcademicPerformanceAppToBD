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
    public class VMRegistration : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private UserController userController;

        public VMRegistration()
        {
            userController = new UserController();
            //LoadData();
            CurrentUser = new UserModel();
            saveCommand = new RelayCommand(Save);
        }

        private ObservableCollection<UserModel> userList;

        public ObservableCollection<UserModel> UserList
        {
            get { return userList; }
            set { userList = value; OnPropertyChanged("UserList"); }

        }

        private void LoadData()
        {
            userList = new ObservableCollection<UserModel>(userController.GetAll());
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
            var password = ((PasswordBox)param).Password;
            currentUser.PasswordUser = password;
            currentUser.RoleUser = 5;

            if (string.IsNullOrEmpty(currentUser.LoginUser))
            {
                Message = "Введите логин";
            }
            else if (string.IsNullOrEmpty(currentUser.PasswordUser))
            {
                Message = "Введите пароль";
            }
            else if (password != App.PasswordUser)
            {
                Message = "Пароли не совпадают";
            }
            else if (userController.IsLoginFree(currentUser.LoginUser))
            {
                try
                {
                    var isSaved = userController.Add(CurrentUser);
                    if (isSaved)
                    {
                        Message = "Регистрация прошла успешна, вы можете использовать свой логин и пароль для входа";
                    }
                    else
                    {
                        Message = "Регистрация закончилась ошибкой, обратитесь к школьному администатору для её устранения или попробуйте снова";
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    throw;
                }
            }
            else
            {
                Message = "Данный логин занят, попробуйте другой";
            }
            if (Message.Length > 0) MessageBox.Show(Message);
        }

    }
}
