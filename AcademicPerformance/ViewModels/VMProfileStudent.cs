using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.Classes;
using AcademicPerformance.Classes.DataAdapters;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.ViewModels
{
    public class VMProfileStudent 
    {
        private readonly StudentAdapter studentAdapter = new StudentAdapter();
        private readonly UserAdapter userAdapter = new UserAdapter();

        public VMProfileStudent()
        {
            CurrentUser = new UserModel();
            AddCommand = new RelayCommand(Add);
            CurrentStudent = studentAdapter.GetById(App.IdUser);
            if (CurrentStudent.DateOfBirthStudent == default) 
                CurrentStudent.DateOfBirthStudent = DateTime.Now;
            if (App.RoleUser != Const.RoleValue.Admin)
            {
                CurrentUser.LoginUser = App.LoginUser;
            }
            CurrentUser.PasswordUser = "";
            CurrentUser.IdUser = App.IdUser;
            CurrentUser.RoleUser = App.RoleUser;
        }


        public StudentModel CurrentStudent { get; set; }

        public UserModel CurrentUser { get; set; }


        public RelayCommand AddCommand { get; }


        public string Message { get; set; }


        public void Add(object param)
        {
            if (param != null)
            {
                var password = ((PasswordBox) param).Password;

                if ((String.IsNullOrWhiteSpace(CurrentStudent.FirstNameStudent)) ||
                    (String.IsNullOrWhiteSpace(CurrentStudent.LastNameStudent)) ||
                    (CurrentStudent.DateOfBirthStudent == DateTime.Now) ||
                    (String.IsNullOrWhiteSpace(CurrentStudent.NumberPhoneStudent)))
                {
                    Message = "Заполните все поля";
                }
                else if (App.RoleUser == Const.RoleValue.Admin)
                {
                    var newStudent = new UserModel
                    {
                        RoleUser = Const.RoleValue.Student,
                        LoginUser = CurrentUser.LoginUser,
                        PasswordUser = password
                    };
                    if (userAdapter.IsLoginFree(newStudent.LoginUser))
                    {
                        userAdapter.DataAccess.InsertUser(newStudent);
                        var last = userAdapter.GetAll()
                            .OrderByDescending(item => item.IdUser).First();
                        CurrentStudent.IdUser = last.IdUser;
                        Message = studentAdapter.Add(CurrentStudent) ? 
                            "Добавлен новый ученик" :
                            "При добавлении произошла ошибка";
                    }
                    else Message = "Логин занят";

                }
                else if (password != App.PasswordUser)
                {
                    Message = "Подтвердите изменения вводом текущего пароля";
                }
                else if (studentAdapter.GetById(CurrentStudent.IdUser).IdStudent == 0)
                {
                    CurrentStudent.IdUser = CurrentUser.IdUser;
                    Message = studentAdapter.Add(CurrentStudent)
                        ? "Добавлен новый ученик"
                        : "При добавлении произошла ошибка";
                    if (App.RoleUser==Const.RoleValue.User)
                    {
                        var newStudent = new UserModel
                        {
                            RoleUser = Const.RoleValue.Student,
                            LoginUser = App.LoginUser,
                            PasswordUser = App.PasswordUser,
                            IdUser = App.IdUser
                        };
                        userAdapter.DataAccess.UpdateUser(newStudent);
                    }
                }
                else if (studentAdapter.Update(CurrentStudent))
                {
                    Message = "Данные обновлены";
                }
                else
                {
                    Message = "При обновлении произошла ошибка";
                }
            }

            MessageBox.Show(Message);
        }

    }
}