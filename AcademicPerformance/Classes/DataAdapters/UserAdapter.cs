using System.Collections.Generic;
using AcademicPerformance.Classes.DataModels;
using AcademicPerformance.Classes.DataSqlGateways;

namespace AcademicPerformance.Classes.DataAdapters
{
    public class UserAdapter
    {
        public UserAdapter()
        {
            DataAccess = new SqlDataAccess();
        }

        public SqlDataAccess DataAccess { get; }

        public bool AddUser(UserModel newUser)
        {
            return DataAccess != null && DataAccess.InsertUser(newUser);
        }

        public bool DeleteUserById(int idUser)
        {
            return DataAccess != null && DataAccess.DeleteUser(idUser);
        }

        public List<UserModel> GetAllUser()
        {
            var userList = DataAccess.GetUserList();
            return userList ?? new List<UserModel>();
        }

        public bool IsUserAuthValid(string login, string password)
        {
            return DataAccess != null && DataAccess.IsAuthValid(login, password);
        }

        public bool IsUserLoginFree(string login)
        {
            return DataAccess != null && DataAccess.IsLoginFree(login);
        }

        public UserModel GetUserById(int idUser)
        {
            return DataAccess != null ? 
                DataAccess.SelectUserId(idUser) : new UserModel();
        }

        public UserModel GetUserByLogin(string loginUser)
        {
            return DataAccess != null ? 
                DataAccess.SelectUserLogin(loginUser) : new UserModel();
        }

        public bool SetUser(UserModel userToUpdate)
        {
            return DataAccess != null && 
                   DataAccess.UpdateUser(userToUpdate);
        }
    }
}