using System.Collections.Generic;

namespace AcademicPerformance.ClassFolder
{
    public class DisciplineController
    {
        public CDataAccess DataAccess { get; }

        public DisciplineController()
        {
            DataAccess = new CDataAccess();
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

        public DisciplineModel SelectId(int idDiscipline)
        {
            return DataAccess != null ? DataAccess.GetDiscipline(idDiscipline) : new DisciplineModel();
        }

    }




}


