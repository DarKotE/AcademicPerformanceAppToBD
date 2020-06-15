using System.Collections.Generic;

namespace AcademicPerformance.ClassFolder
{
    public class JournalController
    {
        public CDataAccess DataAccess { get; }

        public JournalController()
        {
            DataAccess = new CDataAccess();
        }

        public List<JournalModel> GetAll()
        {
            var journalList = DataAccess.GetJournalList();
            return journalList ?? new List<JournalModel>();
        }

        public bool Add(JournalModel journal)
        {
            return DataAccess != null && DataAccess.InsertJournal(journal);
        }

        public bool Update(JournalModel journalToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateJournal(journalToUpdate);
        }

        public bool Delete(int idJournal)
        {
            return DataAccess != null && DataAccess.DeleteJournal(idJournal);
        }

        public JournalModel SelectId(int idJournal)
        {
            return DataAccess != null ? DataAccess.GetJournal(idJournal) : new JournalModel();
        }

    }
}
