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

        public bool Add(JournalModel journal)
        {
            return dataAccess.InsertJournal(journal);
        }

        public bool Update(JournalModel journalToUpdate)
        {
            return dataAccess.UpdateJournal(journalToUpdate);
        }

        public bool Delete(int idJournal)
        {
            return dataAccess.DeleteJournal(idJournal);
        }

        public JournalModel SelectId(int idJournal)
        {
            return dataAccess.GetJournal(idJournal);
        }

    }
}
