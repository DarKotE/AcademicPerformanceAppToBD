using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder
{
    public class VMAuthorization 
    {
        public UserController UserController { get; }

        public VMAuthorization()
        {
            UserController = new UserController();
            CurrentUser = new UserModel();
            AuthCommand = new RelayCommand(Auth);
        }


        public UserModel CurrentUser { get; set; }
        public RelayCommand AuthCommand { get; }
        public string Message { get; set; }


        public void Auth(object param)
        {
            var password = ((PasswordBox) param).Password;
            CurrentUser.PasswordUser = password;
            Message = "";

            if (string.IsNullOrEmpty(CurrentUser.LoginUser))
                Message = "Введите логин";
            else if (string.IsNullOrEmpty(CurrentUser.PasswordUser))
                Message = "Введите пароль";
            else if (UserController.IsAuthValid(CurrentUser.LoginUser, CurrentUser.PasswordUser))
                try
                {
                    CurrentUser = UserController.SelectName(CurrentUser.LoginUser);
                    App.LoginUser = CurrentUser.LoginUser;
                    App.PasswordUser = CurrentUser.PasswordUser;
                    App.IdUser = CurrentUser.IdUser;
                    App.RoleUser = CurrentUser.RoleUser;
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    throw;
                }
            else
                Message = "Логин или пароль не верны, проверьте введённые данные";

            if (!String.IsNullOrEmpty(Message)) MessageBox.Show(Message);
        }
        
    }
}