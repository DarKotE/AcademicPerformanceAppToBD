using System;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.Classes;
using AcademicPerformance.Classes.DataAdapters;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.ViewModels

{
    public class VMRegistration
    {
        private readonly UserAdapter userAdapter;

        public VMRegistration()
        {
            userAdapter = new UserAdapter();
            CurrentUser = new UserModel();
            SaveCommand = new RelayCommand(Save);
        }
        
        public UserModel CurrentUser { get; set; }

        public RelayCommand SaveCommand { get; }

        public string Message { get; set; }
        
        public void Save(object param)
        {
            var password = (param as PasswordBox)?.Password;
            CurrentUser.PasswordUser = password;
            CurrentUser.RoleUser = Const.RoleValue.User;

            if (string.IsNullOrEmpty(CurrentUser.LoginUser))
                Message = "Введите логин";
            else if (string.IsNullOrEmpty(CurrentUser.PasswordUser))
                Message = "Введите пароль";
            else if (password != App.PasswordUser)
                Message = "Пароли не совпадают";
            else if (userAdapter.IsLoginFree(CurrentUser.LoginUser))
                try
                {
                    var isSaved = userAdapter.Add(CurrentUser);
                    if (isSaved)
                        Message = "Регистрация прошла успешна, вы можете использовать" + 
                                  " свой логин и пароль для входа. После заполнения профиля вам будет открыт доступ к журналу";
                    else
                        Message =
                            "Регистрация закончилась ошибкой, обратитесь к администатору для её" 
                            + " устранения или попробуйте снова";
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    throw;
                }
            else
                Message = "Данный логин занят, попробуйте другой";

            if (!String.IsNullOrEmpty(Message)) MessageBox.Show(Message);
        }
    }
}