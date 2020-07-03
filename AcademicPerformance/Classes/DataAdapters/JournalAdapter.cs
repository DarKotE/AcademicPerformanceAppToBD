using System.Collections.Generic;
using AcademicPerformance.Classes.DataModels;
using AcademicPerformance.Classes.DataSqlGateways;

namespace AcademicPerformance.Classes.DataAdapters
{
    public class JournalAdapter
    {
        public JournalAdapter()
        {
            DataAccess = new SqlDataAccess();
        }

        public SqlDataAccess DataAccess { get; }

        public bool AddJournal(JournalModel journal)
        {
            return DataAccess != null && DataAccess.InsertJournal(journal);
        }

        public bool DeleteJournalById(int idJournal)
        {
            return DataAccess != null && DataAccess.DeleteJournal(idJournal);
        }

        public List<JournalModel> GetAllJournalToUser()
        {
            var journalList = DataAccess.GetJournalListForUser();
            return journalList ?? new List<JournalModel>();
        }

        public List<JournalModel> GetAllJournalFull()
        {
            var journalList = DataAccess.GetJournalList();
            return journalList ?? new List<JournalModel>();
        }

        public JournalModel GetJournalById(int idJournal)
        {
            return DataAccess != null ? 
                DataAccess.GetJournal(idJournal) : new JournalModel();
        }

        public bool SetJournal(JournalModel journalToUpdate)
        {
            return DataAccess != null && 
                   DataAccess.UpdateJournal(journalToUpdate);
        }
    }
}