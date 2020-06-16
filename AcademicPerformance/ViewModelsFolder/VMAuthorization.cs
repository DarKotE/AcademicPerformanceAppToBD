using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder
{
    public class VMAuthorization : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public UserController UserController { get; }

        public VMAuthorization()
        {
            UserController = new UserController();
            CurrentUser = new UserModel();
            AuthCommand = new RelayCommand(Auth);
        }


        private UserModel currentUser;

        public UserModel CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }


        public RelayCommand AuthCommand { get; }


        private string message;

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged(Message);
            }
        }


        public void Auth(object param)
        {
            var password = ((PasswordBox) param).Password;
            currentUser.PasswordUser = password;
            Message = "";

            if (string.IsNullOrEmpty(currentUser.LoginUser))
                Message = "Введите логин";
            else if (string.IsNullOrEmpty(currentUser.PasswordUser))
                Message = "Введите пароль";
            else if (UserController.IsAuthValid(currentUser.LoginUser, currentUser.PasswordUser))
                try
                {
                    currentUser = UserController.SelectName(CurrentUser.LoginUser);
                    App.LoginUser = currentUser.LoginUser;
                    App.PasswordUser = currentUser.PasswordUser;
                    App.IdUser = currentUser.IdUser;
                    App.RoleUser = currentUser.RoleUser;
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    throw;
                }
            else
                Message = "Логин или пароль не верны, проверьте введённые данные";

            if (Message.Length > 0) MessageBox.Show(Message);
        }
    }
}