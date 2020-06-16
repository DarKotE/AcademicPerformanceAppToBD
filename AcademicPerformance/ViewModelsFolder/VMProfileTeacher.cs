using System;
using System.ComponentModel;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;
using System.Windows;
using System.Windows.Controls;

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

        public VMProfileTeacher()
        {

            TeacherController = new TeacherController();
            CurrentUser = new UserModel();
            CurrentTeacher = TeacherController.Select(App.IdUser);
            if (CurrentTeacher.DateOfBirthTeacher == default)
            {
                CurrentTeacher.DateOfBirthTeacher = DateTime.Now;
            }
            CurrentUser.LoginUser = App.LoginUser;
            CurrentUser.PasswordUser = "0";
            CurrentUser.IdUser = App.IdUser;
            CurrentUser.RoleUser = App.RoleUser;


            addCommand = new RelayCommand(Add);
        }


        private TeacherModel currentTeacher;
        public TeacherModel CurrentTeacher
        {
            get { return currentTeacher; }
            set
            {
                currentTeacher = value;
                OnPropertyChanged("CurrentTeacher");
            }
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


        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get { return addCommand; }
        }


        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }


        public void Add(object param)
        {
            var password = ((PasswordBox)param).Password;
            if (password != App.PasswordUser)
            {
                Message = "Подтвердите изменения вводом текущего пароля";
            }
            else if (TeacherController.Select(CurrentTeacher.IdUser).IdTeacher == 0)
            {
                CurrentTeacher.IdUser = CurrentUser.IdUser;
                Message = TeacherController.Add(CurrentTeacher) ? "Добавлен новый ученик" : "При добавлении произошла ошибка";
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
