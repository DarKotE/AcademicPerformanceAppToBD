using System.ComponentModel;
namespace AcademicPerformance.ClassFolder
{
    public class UserModel:INotifyPropertyChanged
    {
        
        private int idUser;
        public int IdUser
        {
            get { return idUser;}
            set { idUser = value; OnPropertyChanged("IdUser"); }
        }
        private string loginUser;
        public string LoginUser
        {
            get { return loginUser; }
            set { loginUser = value; OnPropertyChanged("LoginUser"); }
        }
        private int roleUser;
        public int RoleUser
        {
            get { return roleUser; }
            set { roleUser = value; OnPropertyChanged("RoleUser"); }
        }
        private string passwordUser;
        public string PasswordUser
        {
            get { return passwordUser; }
            set { passwordUser = value; OnPropertyChanged("PasswordUser"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
