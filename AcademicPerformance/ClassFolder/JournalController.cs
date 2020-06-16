using System.Collections.Generic;

namespace AcademicPerformance.ClassFolder
{
    public class JournalController
    {
        public JournalController()
        {
            DataAccess = new CDataAccess();
        }

        public CDataAccess DataAccess { get; }

        public bool Add(JournalModel journal)
        {
            return DataAccess != null && DataAccess.InsertJournal(journal);
        }

        public bool Delete(int idJournal)
        {
            return DataAccess != null && DataAccess.DeleteJournal(idJournal);
        }

        public List<JournalModel> GetAll()
        {
            var journalList = DataAccess.GetJournalListForUser();
            return journalList ?? new List<JournalModel>();
        }

        public List<JournalModel> GetAllFull()
        {
            var journalList = DataAccess.GetJournalList();
            return journalList ?? new List<JournalModel>();
        }

        public JournalModel SelectId(int idJournal)
        {
            return DataAccess != null ? DataAccess.GetJournal(idJournal) : new JournalModel();
        }

        public bool Update(JournalModel journalToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateJournal(journalToUpdate);
        }
    }
}