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
            List<EvaluationModel> evaluationList = new List<EvaluationModel>();
            evaluationList = dataAccess.GetEvaluationList();
            return evaluationList;
        }

        public bool Add(EvaluationModel newEvaluation)
        {
            return dataAccess.InsertEvaluation(newEvaluation);
        }

        public bool Update(EvaluationModel evaluationToUpdate)
        {
            return dataAccess.UpdateEvaluation(evaluationToUpdate);
        }

        public bool Delete(int idEvaluation)
        {
            return dataAccess.DeleteEvaluation(idEvaluation);
        }

        public EvaluationModel SelectId(int idEvaluation)
        {
            return dataAccess.GetEvaluation(idEvaluation);
        }

    }
}
