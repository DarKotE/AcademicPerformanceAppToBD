using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder
{
    public class VMProfileStudent : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly StudentController studentController;
        private readonly UserController userController;

        public VMProfileStudent()
        {
            studentController = new StudentController();
            userController = new UserController();
            CurrentUser = new UserModel();
            CurrentStudent = studentController.Select(App.IdUser);
            if (CurrentStudent.DateOfBirthStudent == default) CurrentStudent.DateOfBirthStudent = DateTime.Now;
            CurrentUser.LoginUser = App.LoginUser;
            CurrentUser.PasswordUser = "0";
            CurrentUser.IdUser = App.IdUser;
            CurrentUser.RoleUser = App.RoleUser;

            


            AddCommand = new RelayCommand(Add);
        }


        private StudentModel currentStudent;

        public StudentModel CurrentStudent
        {
            get => currentStudent;
            set
            {
                currentStudent = value;
                OnPropertyChanged("CurrentStudent");
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
            var password = ((PasswordBox) param).Password;
            if ((CurrentStudent.FirstNameStudent == "") || (CurrentStudent.FirstNameStudent == default) ||
                (CurrentStudent.LastNameStudent == "") || (CurrentStudent.LastNameStudent == default) ||
                (CurrentStudent.DateOfBirthStudent == DateTime.Now) ||
                (CurrentStudent.NumberPhoneStudent == "") || (CurrentStudent.NumberPhoneStudent == default))
            {
                Message = "Заполните все поля";
            }
            else if (password != App.PasswordUser)
            {
                Message = "Подтвердите изменения вводом текущего пароля";
            }
            else if (App.RoleUser==3)
            {
                var newStudent = new UserModel();
                newStudent.RoleUser = 4;
                newStudent.LoginUser = CurrentUser.LoginUser;
                newStudent.PasswordUser = password;
                if (userController.IsLoginFree(newStudent.LoginUser))
                {
                    userController.DataAccess.InsertUser(newStudent);
                    var last = userController.GetAll().OrderByDescending(item => item.IdUser).First();
                    CurrentStudent.IdUser = last.IdUser;
                    Message = studentController.Add(CurrentStudent) ? "Добавлен новый ученик"
                        : "При добавлении произошла ошибка";
                }
                else Message = "Логин занят";

            }
            else if (studentController.Select(CurrentStudent.IdUser).IdStudent == 0)
            {
                CurrentStudent.IdUser = CurrentUser.IdUser;
                Message = studentController.Add(CurrentStudent)
                    ? "Добавлен новый ученик"
                    : "При добавлении произошла ошибка";
                if (App.RoleUser==1)
                {
                    var userController = new UserController();
                    var newStudent = new UserModel();
                    newStudent.RoleUser = 4;
                    newStudent.LoginUser = App.LoginUser;
                    newStudent.PasswordUser = App.PasswordUser;
                    newStudent.IdUser = App.IdUser;
                    userController.DataAccess.UpdateUser(newStudent);
                }
            }
            else if (studentController.Update(CurrentStudent))
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