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

        public bool Add(DisciplineModel newDiscipline)
        {
            return DataAccess != null && DataAccess.InsertDiscipline(newDiscipline);
        }

        public bool Update(DisciplineModel disciplineToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateDiscipline(disciplineToUpdate);
        }

        public bool Delete(int idDiscipline)
        {
            return DataAccess != null && DataAccess.DeleteDiscipline(idDiscipline);
        }

        public DisciplineModel GetById(int idDiscipline)
        {
            return DataAccess != null 
                ? DataAccess.GetDiscipline(idDiscipline)
                : new DisciplineModel();
        }
    }
}