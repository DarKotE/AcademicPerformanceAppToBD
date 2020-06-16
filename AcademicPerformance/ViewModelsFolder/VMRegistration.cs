using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder

{
    public class VMRegistration : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly UserController userController;

        public VMRegistration()
        {
            userController = new UserController();
            CurrentUser = new UserModel();
            SaveCommand = new RelayCommand(Save);
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

        public RelayCommand SaveCommand { get; }

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


        public void Save(object param)
        {
            var password = ((PasswordBox) param).Password;
            currentUser.PasswordUser = password;
            currentUser.RoleUser = 4;

            if (string.IsNullOrEmpty(currentUser.LoginUser))
                Message = "Введите логин";
            else if (string.IsNullOrEmpty(currentUser.PasswordUser))
                Message = "Введите пароль";
            else if (password != App.PasswordUser)
                Message = "Пароли не совпадают";
            else if (userController.IsLoginFree(currentUser.LoginUser))
                try
                {
                    var isSaved = userController.Add(CurrentUser);
                    if (isSaved)
                        Message = "Регистрация прошла успешна, вы можете использовать свой логин и пароль для входа";
                    else
                        Message =
                            "Регистрация закончилась ошибкой, обратитесь к школьному администатору для её" 
                            + " устранения или попробуйте снова";
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    throw;
                }
            else
                Message = "Данный логин занят, попробуйте другой";

            if (Message.Length > 0) MessageBox.Show(Message);
        }
    }
}