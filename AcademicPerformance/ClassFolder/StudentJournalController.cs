using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicPerformance.ClassFolder
{
    class StudentJournalController
    {
        CDataAccess dataAccess = new CDataAccess();

        public StudentJournalController()
        {

        }

        public List<StudentJournalModel> GetAll()
        {
            List<StudentJournalModel> journalList = new List<StudentJournalModel>();
            journalList = dataAccess.GetJournalList();
            return journalList;
        }

        //public bool AddUser(UserModel newUser)
        //{
        //    return dataAccess.InsertUser(newUser);
        //}

        //public bool Update(UserModel userToUpdate)
        //{
        //    return dataAccess.UpdateUser(userToUpdate);
        //}

        //public bool Delete(int idUser)
        //{
        //    return dataAccess.DeleteUser(idUser);
        //}

        //public UserModel SelectUserId(int idUser)
        //{
        //    return dataAccess.SelectUserId(idUser);
        //}
        //public UserModel SelectUserLogin(string loginUser)
        //{
        //    return dataAccess.SelectUserLogin(loginUser);
        //}

    }
}
