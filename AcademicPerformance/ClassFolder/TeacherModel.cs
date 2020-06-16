using System;
using System.ComponentModel;

namespace AcademicPerformance.ClassFolder
{
    public class TeacherModel : INotifyPropertyChanged
    {

        private int idUser;
        public int IdUser
        {
            get { return idUser; }
            set { idUser = value; OnPropertyChanged("IdUser"); }
        }
        private int idTeacher;
        public int IdTeacher
        {
            get { return idTeacher; }
            set { idTeacher = value; OnPropertyChanged("IdTeacher"); }
        }
        private string lastNameTeacher;
        public string LastNameTeacher
        {
            get { return lastNameTeacher; }
            set { lastNameTeacher = value; OnPropertyChanged("LastNameTeacher"); }
        }
        private string firstNameTeacher;
        public string FirstNameTeacher
        {
            get { return firstNameTeacher; }
            set { firstNameTeacher = value; OnPropertyChanged("FirstNameTeacher"); }
        }
        private string middleNameTeacher;
        public string MiddleNameTeacher
        {
            get { return middleNameTeacher; }
            set { middleNameTeacher = value; OnPropertyChanged("MiddleNameTeacher"); }
        }
        private DateTime dateOfBirthTeacher;
        public DateTime DateOfBirthTeacher
        {
            get { return dateOfBirthTeacher; }
            set { dateOfBirthTeacher = value; OnPropertyChanged("DateOfBirthTeacher"); }
        }

        private string numberPhoneTeacher;
        public string NumberPhoneTeacher
        {
            get { return numberPhoneTeacher; }
            set { numberPhoneTeacher = value; OnPropertyChanged("NumberPhoneTeacher"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        //Поле отсутствует в бд
        private string fullName;
        public string FullName
        {
            
            get
            {
                fullName = LastNameTeacher;
                fullName += " " + FirstNameTeacher;
                if (MiddleNameTeacher != default) fullName += " " + MiddleNameTeacher;
                return fullName;
            }
            set
            {
                fullName = value;

            }
        }


    }
}
