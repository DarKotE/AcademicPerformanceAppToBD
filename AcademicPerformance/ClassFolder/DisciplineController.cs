using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicPerformance.ClassFolder
{
    public class DisciplineController
    {
        CDataAccess dataAccess = new CDataAccess();

        public DisciplineController()
        {

        }

        public List<DisciplineModel> GetAll()
        {
            List<DisciplineModel> disciplineList = new List<DisciplineModel>();
            disciplineList = dataAccess.GetDisciplineList();
            return disciplineList;
        }
        
        public bool Add(DisciplineModel newDiscipline)
        {
            return dataAccess.InsertDiscipline(newDiscipline);
        }

        public bool Update(DisciplineModel disciplineToUpdate)
        {
            return dataAccess.UpdateDiscipline(disciplineToUpdate);
        }

        public bool Delete(int idDiscipline)
        {
            return dataAccess.DeleteDiscipline(idDiscipline);
        }

        public DisciplineModel SelectId(int idDiscipline)
        {
            return dataAccess.GetDiscipline(idDiscipline);
        }

    }




}


