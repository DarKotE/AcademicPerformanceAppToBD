using System.Collections.Generic;
using AcademicPerformance.Classes.DataModels;
using AcademicPerformance.Classes.DataSqlGateways;

namespace AcademicPerformance.Classes.DataAdapters
{
    public class DisciplineAdapter
    {
        public SqlDataAccess DataAccess { get; }

        public DisciplineAdapter()
        {
            DataAccess = new SqlDataAccess();
        }

        public List<DisciplineModel> GetAll()
        {
            var disciplineList = DataAccess.GetDisciplineList();
            return disciplineList ?? new List<DisciplineModel>();
        }

        public bool AddDiscipline(DisciplineModel newDiscipline)
        {
            return DataAccess != null && DataAccess.InsertDiscipline(newDiscipline);
        }

        public bool SetDiscipline(DisciplineModel disciplineToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateDiscipline(disciplineToUpdate);
        }

        public bool DeleteDisciplineById(int idDiscipline)
        {
            return DataAccess != null && DataAccess.DeleteDiscipline(idDiscipline);
        }

        public DisciplineModel GetDisciplineById(int idDiscipline)
        {
            return DataAccess != null 
                ? DataAccess.GetDiscipline(idDiscipline)
                : new DisciplineModel();
        }
    }
}