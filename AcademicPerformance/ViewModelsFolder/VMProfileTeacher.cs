using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder
{
    public class VMProfileTeacher 
    {
        private readonly TeacherController teacherController = new TeacherController();
        private readonly UserController userController = new UserController();

        public VMProfileTeacher()
        {
            CurrentUser = new UserModel();
            AddCommand = new RelayCommand(Add);
            CurrentTeacher = teacherController.Select(App.IdUser);
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
            
            var password = ((PasswordBox)param).Password;
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
                if (userController.IsLoginFree(newTeacher.LoginUser))
                {
                    userController.DataAccess.InsertUser(newTeacher);
                    var last = userController.GetAll().OrderByDescending(
                        item => item.IdUser).First();
                    CurrentTeacher.IdUser = last.IdUser;
                    Message = teacherController.Add(CurrentTeacher)
                        ? "Добавлен новый преподаватель"
                        : "При добавлении произошла ошибка";
                }
                else Message = "Логин занят";
            }
            else if (password != App.PasswordUser)
            {
                Message = "Подтвердите изменения вводом текущего пароля";
            }
            else if (teacherController.Select(CurrentTeacher.IdUser).IdTeacher == 0)
            {
                CurrentTeacher.IdUser = CurrentUser.IdUser;
                Message = teacherController.Add(CurrentTeacher)
                    ? "Добавлен новый ученик"
                    : "При добавлении произошла ошибка";
            }
            else if (teacherController.Update(CurrentTeacher))
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