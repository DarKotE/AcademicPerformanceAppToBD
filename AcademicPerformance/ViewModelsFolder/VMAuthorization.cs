using System;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace AcademicPerformance.ViewModelsFolder
{
    public class VMAuthorization : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        
        private UserController userController;
        public VMAuthorization()
        {
            
            userController = new UserController();
            CurrentUser = new UserModel();
            authCommand = new RelayCommand(Auth);
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


        private RelayCommand authCommand;
        public RelayCommand AuthCommand
        {
            get { return authCommand; }
        }


        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(Message); }
        }
        

        public void Auth(object param)
        {
            var password = ((PasswordBox)param).Password;
            currentUser.PasswordUser = password;
            Message = "";

            if (string.IsNullOrEmpty(currentUser.LoginUser))
            {
                Message = "Введите логин";
            }
            else if (string.IsNullOrEmpty(currentUser.PasswordUser))
            {
                Message = "Введите пароль";
            }
            else if (userController.IsAuthValid(currentUser.LoginUser,currentUser.PasswordUser))
            {
                try
                {
                    currentUser = userController.SelectName(CurrentUser.LoginUser);
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
            }
            else
            {
                Message = "Логин или пароль не верны, проверьте введённые данные";
            }
            if (Message.Length>0) MessageBox.Show(Message);
        }

    }
}
