using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicPerformance.ClassFolder
{

    public class EvaluationController
    {
        CDataAccess dataAccess = new CDataAccess();

        public EvaluationController()
        {

        }

        public List<EvaluationModel> GetAll()
        {
            List<EvaluationModel> disciplineList = new List<EvaluationModel>();
            disciplineList = dataAccess.GetDisciplineList();
            return disciplineList;
        }

        public bool Add(EvaluationModel newEvaluation)
        {
            return dataAccess.InsertDiscipline(newEvaluation);
        }

        public bool Update(EvaluationModel evaluationToUpdate)
        {
            return dataAccess.UpdateDiscipline(evaluationToUpdate);
        }

        public bool Delete(int idEvaluation)
        {
            return dataAccess.DeleteDiscipline(idEvaluation);
        }

        public EvaluationModel SelectId(int idEvaluation)
        {
            return dataAccess.GetDiscipline(idEvaluation);
        }

    }
