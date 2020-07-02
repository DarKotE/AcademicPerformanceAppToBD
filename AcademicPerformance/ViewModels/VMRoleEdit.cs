using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AcademicPerformance.Classes;
using AcademicPerformance.Classes.DataAdapters;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.ViewModels

{
    public class VMRoleEdit : INotifyPropertyChanged
    {
        
        public VMRoleEdit()
        {
            UserAdapter = new UserAdapter();
            RoleAdapter = new RoleAdapter();
            TeacherAdapter = new TeacherAdapter();
            StudentAdapter = new StudentAdapter();;
            SelectedRole = new RoleModel();
            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
            LoadData();
            Filter();
        }

        public UserAdapter UserAdapter { get; }
        public TeacherAdapter TeacherAdapter { get; }
        public  StudentAdapter StudentAdapter { get; }
        public RoleAdapter RoleAdapter { get; }

        private ObservableCollection<UserModel> filteredUserList;
        public ObservableCollection<UserModel> FilteredUserList
        {
            get => filteredUserList;
            set
            {
                filteredUserList = value;
                OnPropertyChanged(nameof(FilteredUserList));
            }
        }


        public ObservableCollection<UserModel> UserList { get; set; }

        public ObservableCollection<RoleModel> RoleList { get; set; }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Filter();
                OnPropertyChanged(nameof(SearchText));
            }
        }

        private UserModel selectedRow;
        public UserModel SelectedRow
        {
            get => selectedRow;
            set
            {
                selectedRow = value;
                OnPropertyChanged(nameof(SelectedRow));
            }
        }


        public RelayCommand SaveCommand { get; }

        public string Message { get; set; }


        private RoleModel selectedRole;
        public RoleModel SelectedRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                
                if (RoleList == null || selectedRole == null || SelectedRow == null ||
                    SelectedRow.RoleUser == selectedRole.IdRole) return;
                SelectedRow.RoleUser = selectedRole.IdRole;
                OnPropertyChanged(nameof(SelectedRole));
            }
        }



        public RelayCommand DeleteCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Filter()
        {
            FilteredUserList =
                new ObservableCollection<UserModel>(
                    from item
                    in UserList
                    where item.LoginUser.ToUpper().Contains(SearchText.ToUpper())
                          || item.IdUser.ToString().ToUpper().Contains(SearchText.ToUpper())
                          || item.RoleUser.ToString().ToUpper().Contains(SearchText.ToUpper())
                    select item);
            if (FilteredUserList.Any()) SelectedRow = FilteredUserList[0];
            
        }


        private void LoadData()
        {
            UserList = new ObservableCollection<UserModel>(UserAdapter.GetAll());
            RoleList = new ObservableCollection<RoleModel>(RoleAdapter.GetAll());
            SearchText = "";
        }
        
        public void Save(object param)
        {
            var isAllSaved = true;
            foreach (var item in filteredUserList)
                if (!UserAdapter.Update(item))
                    isAllSaved = false;

            Message = isAllSaved ? "Изменения сохранены" 
                : "При сохранении произошла ошибка";
            MessageBox.Show(Message);
            LoadData();
        }

        public void Delete(object param)
        {
            var isDeleted =
                (StudentAdapter.Delete(SelectedRow.IdUser) || TeacherAdapter.Delete(SelectedRow.IdUser)) &&
                (UserAdapter.Delete(SelectedRow.IdUser));
            Message = isDeleted ? "Удалено" 
                : "При удалении произошла ошибка";
            MessageBox.Show(Message);
            LoadData();
        }
    }
}