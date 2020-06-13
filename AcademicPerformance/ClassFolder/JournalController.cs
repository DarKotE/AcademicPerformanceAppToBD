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

        //public bool Add(UserModel newUser)
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

        //public UserModel SelectId(int idUser)
        //{
        //    return dataAccess.SelectId(idUser);
        //}
        //public UserModel SelectName(string loginUser)
        //{
        //    return dataAccess.SelectName(loginUser);
        //}

    }
}
