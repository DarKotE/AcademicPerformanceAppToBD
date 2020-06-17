using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder
{
    public class VMProfileTeacher : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly TeacherController TeacherController;
        private readonly UserController userController;

        public VMProfileTeacher()
        {
            TeacherController = new TeacherController();
            userController = new UserController();
            CurrentUser = new UserModel();
            CurrentTeacher = TeacherController.Select(App.IdUser);
            if (CurrentTeacher.DateOfBirthTeacher == default) 
                CurrentTeacher.DateOfBirthTeacher = DateTime.Now;
            CurrentUser.LoginUser = App.LoginUser;
            CurrentUser.PasswordUser = "0";
            CurrentUser.IdUser = App.IdUser;
            CurrentUser.RoleUser = App.RoleUser;


            AddCommand = new RelayCommand(Add);
        }


        private TeacherModel currentTeacher;

        public TeacherModel CurrentTeacher
        {
            get => currentTeacher;
            set
            {
                currentTeacher = value;
                OnPropertyChanged("CurrentTeacher");
            }
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


        public RelayCommand AddCommand { get; }


        private string message;

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }


        public void Add(object param)
        {
            
            var password = ((PasswordBox)param).Password;
            if ((CurrentTeacher.FirstNameTeacher == "") || (CurrentTeacher.FirstNameTeacher == default) ||
                (CurrentTeacher.LastNameTeacher == "") || (CurrentTeacher.LastNameTeacher == default) ||
                (CurrentTeacher.DateOfBirthTeacher == DateTime.Now) ||
                (CurrentTeacher.NumberPhoneTeacher == "") || (CurrentTeacher.NumberPhoneTeacher == default))
            {
                Message = "Заполните все поля";
            }
            else if (App.RoleUser == 3)
            {
                var newTeacher = new UserModel();
                newTeacher.RoleUser = 4;
                newTeacher.LoginUser = CurrentUser.LoginUser;
                newTeacher.PasswordUser = password;
                if (userController.IsLoginFree(newTeacher.LoginUser))
                {
                    userController.DataAccess.InsertUser(newTeacher);
                    var last = userController.GetAll().OrderByDescending(item => item.IdUser).First();
                    CurrentTeacher.IdUser = last.IdUser;
                    Message = TeacherController.Add(CurrentTeacher)
                        ? "Добавлен новый преподаватель"
                        : "При добавлении произошла ошибка";
                }
                else Message = "Логин занят";


            }

            else
            if (password != App.PasswordUser)
            {
                Message = "Подтвердите изменения вводом текущего пароля";
            }
            else if (TeacherController.Select(CurrentTeacher.IdUser).IdTeacher == 0)
            {
                CurrentTeacher.IdUser = CurrentUser.IdUser;
                Message = TeacherController.Add(CurrentTeacher)
                    ? "Добавлен новый ученик"
                    : "При добавлении произошла ошибка";
            }
            else if (TeacherController.Update(CurrentTeacher))
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