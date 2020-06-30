using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.CommandsFolder;

namespace AcademicPerformance.ViewModelsFolder

{
    public class VMRoleEdit : INotifyPropertyChanged
    {
        private string searchText;

        public VMRoleEdit()
        {
            UserController = new UserController();
            RoleController = new RoleController();
            TeacherController = new TeacherController();
            StudentController = new StudentController();;
            SelectedRole = new RoleModel();
            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
            LoadData();
            Filter();
        }

        public UserController UserController { get; }
        public TeacherController TeacherController { get; }
        public  StudentController StudentController { get; }
        public RoleController RoleController { get; }

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
            UserList = new ObservableCollection<UserModel>(UserController.GetAll());
            RoleList = new ObservableCollection<RoleModel>(RoleController.GetAll());
            SearchText = "";
        }
        
        public void Save(object param)
        {
            var isAllSaved = true;
            foreach (var item in filteredUserList)
                if (!UserController.Update(item))
                    isAllSaved = false;

            Message = isAllSaved ? "Изменения сохранены" : "При сохранении произошла ошибка";
            MessageBox.Show(Message);
            LoadData();
        }

        public void Delete(object param)
        {
            var isDeleted =
                (StudentController.Delete(SelectedRow.IdUser) || TeacherController.Delete(SelectedRow.IdUser)) &&
                (UserController.Delete(SelectedRow.IdUser));
            Message = isDeleted ? "Удалено" : "При удалении произошла ошибка";
            MessageBox.Show(Message);
            LoadData();
        }
    }
}