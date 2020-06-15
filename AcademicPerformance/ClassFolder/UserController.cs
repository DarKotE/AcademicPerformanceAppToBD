using System.Collections.Generic;

namespace AcademicPerformance.ClassFolder
{

    public class UserController
    {
        public CDataAccess DataAccess { get; }

        public UserController()
        {
            DataAccess = new CDataAccess();
        }

        public List<UserModel> GetAll()
        {
            var userList = DataAccess.GetUserList();
            return userList ?? new List<UserModel>();
        }

        public bool IsLoginFree(string login)
        {
            return DataAccess != null && DataAccess.IsLoginFree(login);
        }
        public bool IsAuthValid(string login,string password)
        {
            return DataAccess != null && DataAccess.IsAuthValid(login,password);
        }

        public bool Add(UserModel newUser)
        {
            return DataAccess != null && DataAccess.InsertUser(newUser);
        }

        public bool Update(UserModel userToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateUser(userToUpdate);
        }

        public bool Delete(int idUser)
        {
            return DataAccess != null && DataAccess.DeleteUser(idUser);
        }

        public UserModel SelectId(int idUser)
        {
            return DataAccess != null ? DataAccess.SelectUserId(idUser) : new UserModel();
        }
        public UserModel SelectName(string loginUser)
        {
            return DataAccess != null ? DataAccess.SelectUserLogin(loginUser) : new UserModel();
        }
    }
}
