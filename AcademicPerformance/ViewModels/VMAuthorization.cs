using System;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.Classes;
using AcademicPerformance.Classes.DataAdapters;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.ViewModels
{
    public class VMAuthorization 
    {
        public UserAdapter UserAdapter { get; }

        public VMAuthorization()
        {
            UserAdapter = new UserAdapter();
            CurrentUser = new UserModel();
            AuthCommand = new RelayCommand(Auth);
        }


        public UserModel CurrentUser { get; set; }
        public RelayCommand AuthCommand { get; }
        public string Message { get; set; }


        public void Auth(object param)
        {
            if (param != null)
            {
                var password = ((PasswordBox) param).Password;
                CurrentUser.PasswordUser = password;
            }

            Message = "";

            if (string.IsNullOrWhiteSpace(CurrentUser.LoginUser))
                Message = "Введите логин";
            else if (string.IsNullOrWhiteSpace(CurrentUser.PasswordUser))
                Message = "Введите пароль";
            else if (UserAdapter.IsAuthValid(CurrentUser.LoginUser, CurrentUser.PasswordUser))
                try
                {
                    CurrentUser = UserAdapter.GetByLogin(CurrentUser.LoginUser);
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

            if (!String.IsNullOrWhiteSpace(Message)) MessageBox.Show(Message);
        }
        
    }
}