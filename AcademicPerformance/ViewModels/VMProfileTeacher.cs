using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.Classes;
using AcademicPerformance.Classes.DataAdapters;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.ViewModels
{
    public class VMProfileTeacher 
    {
        private readonly TeacherAdapter teacherAdapter = new TeacherAdapter();
        private readonly UserAdapter userAdapter = new UserAdapter();

        public VMProfileTeacher()
        {
            CurrentUser = new UserModel();
            AddCommand = new RelayCommand(Add);
            CurrentTeacher = teacherAdapter.Select(App.IdUser);
            if (CurrentTeacher.DateOfBirthTeacher == default)
            {
                CurrentTeacher.DateOfBirthTeacher = DateTime.Now;
            }
            if (App.RoleUser != Const.RoleValue.Admin)
            {
                CurrentUser.LoginUser = App.LoginUser;
            }
            CurrentUser.PasswordUser = "";
            CurrentUser.IdUser = App.IdUser;
            CurrentUser.RoleUser = App.RoleUser;
        }

        public TeacherModel CurrentTeacher { get; set; }

        public UserModel CurrentUser { get; set; }

        public RelayCommand AddCommand { get; }


        public string Message { get; set; }

        public void Add(object param)
        {
            
            var password = (param as PasswordBox)?.Password;
            if ((String.IsNullOrWhiteSpace(CurrentTeacher.FirstNameTeacher)) ||
                (String.IsNullOrWhiteSpace(CurrentTeacher.LastNameTeacher)) ||
                (CurrentTeacher.DateOfBirthTeacher == DateTime.Now) ||
                (String.IsNullOrWhiteSpace(CurrentTeacher.NumberPhoneTeacher)))
            {
                Message = "Заполните все поля";
            }
            else if (App.RoleUser == Const.RoleValue.Admin)
            {
                var newTeacher = new UserModel
                {
                    RoleUser = Const.RoleValue.Teacher, 
                    LoginUser = CurrentUser.LoginUser, 
                    PasswordUser = password
                };
                if (userAdapter.IsLoginFree(newTeacher.LoginUser))
                {
                    userAdapter.DataAccess.InsertUser(newTeacher);
                    var last = userAdapter.GetAll().OrderByDescending(
                        item => item.IdUser).First();
                    CurrentTeacher.IdUser = last.IdUser;
                    Message = teacherAdapter.Add(CurrentTeacher)
                        ? "Добавлен новый преподаватель"
                        : "При добавлении произошла ошибка";
                }
                else Message = "Логин занят";
            }
            else if (password != App.PasswordUser)
            {
                Message = "Подтвердите изменения вводом текущего пароля";
            }
            else if (teacherAdapter.Select(CurrentTeacher.IdUser).IdTeacher == 0)
            {
                CurrentTeacher.IdUser = CurrentUser.IdUser;
                Message = teacherAdapter.Add(CurrentTeacher)
                    ? "Добавлен новый ученик"
                    : "При добавлении произошла ошибка";
            }
            else if (teacherAdapter.Update(CurrentTeacher))
            {
                Message = "Данные обновлены";
            }
            else
            {
                Message = "При обновлении произошла ошибка";
            }

            MessageBox.Show(Message);
        }

    }
}