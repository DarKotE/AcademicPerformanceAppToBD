using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicPerformance.ClassFolder
{
    class JournalController
    {
        CDataAccess dataAccess = new CDataAccess();

        public JournalController()
        {

        }

        public List<JournalModel> GetAll()
        {
            List<JournalModel> journalList = new List<JournalModel>();
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
