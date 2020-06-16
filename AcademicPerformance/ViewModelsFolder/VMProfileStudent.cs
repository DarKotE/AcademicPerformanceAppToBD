using System;
using System.ComponentModel;
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

        public VMProfileStudent()
        {
            studentController = new StudentController();
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
            if (password != App.PasswordUser)
            {
                Message = "Подтвердите изменения вводом текущего пароля";
            }
            else if (studentController.Select(CurrentStudent.IdUser).IdStudent == 0)
            {
                CurrentStudent.IdUser = CurrentUser.IdUser;
                Message = studentController.Add(CurrentStudent)
                    ? "Добавлен новый ученик"
                    : "При добавлении произошла ошибка";
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