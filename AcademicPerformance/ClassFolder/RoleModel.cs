using System.ComponentModel;

namespace AcademicPerformance.ClassFolder
{
    public class RoleModel : INotifyPropertyChanged
    {

        private int idRole;
        public int IdRole
        {
            get { return idRole; }
            set { idRole = value; OnPropertyChanged("IdRole"); }
        }

        private string nameRole;
        public string NameRole
        {
            get { return nameRole; }
            set { nameRole = value; OnPropertyChanged("NameRole"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}

