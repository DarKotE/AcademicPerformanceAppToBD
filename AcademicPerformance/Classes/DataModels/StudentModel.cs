using System;
using System.ComponentModel;

namespace AcademicPerformance.Classes.DataModels
{
    public class StudentModel : INotifyPropertyChanged
    {

        private int idUser;
        public int IdUser
        {
            get { return idUser; }
            set { idUser = value;
                OnPropertyChanged(nameof(IdUser)); }
        }
        private int idStudent;
        public int IdStudent
        {
            get { return idStudent; }
            set { idStudent = value;
                OnPropertyChanged(nameof(IdStudent)); }
        }
        private string lastNameStudent;
        public string LastNameStudent
        {
            get { return lastNameStudent; }
            set { lastNameStudent = value; 
                OnPropertyChanged(nameof(LastNameStudent)); }
        }
        private string firstNameStudent;
        public string FirstNameStudent
        {
            get { return firstNameStudent; }
            set { firstNameStudent = value; 
                OnPropertyChanged(nameof(FirstNameStudent)); }
        }
        private string middleNameStudent;
        public string MiddleNameStudent
        {
            get { return middleNameStudent; }
            set { middleNameStudent = value; 
                OnPropertyChanged(nameof(MiddleNameStudent)); }
        }
        private DateTime dateOfBirthStudent;
        public DateTime DateOfBirthStudent
        {
            get { return dateOfBirthStudent; }
            set { dateOfBirthStudent = value;
                OnPropertyChanged(nameof(DateOfBirthStudent)); }
        }

        private string numberPhoneStudent;
        public string NumberPhoneStudent
        {
            get { return numberPhoneStudent; }
            set { numberPhoneStudent = value;
                OnPropertyChanged(nameof(NumberPhoneStudent)); }
        }

        //Поле отсутствует в бд
        private string fullName;
        public string FullName
        {
            get
            {
                if (LastNameStudent != null) fullName = LastNameStudent;
                if (FirstNameStudent != default) fullName += " " + FirstNameStudent;
                if (MiddleNameStudent != null) fullName += " " + MiddleNameStudent;
                return fullName;
            }
            set
            {
                fullName = value;
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));

        }
    }


}
