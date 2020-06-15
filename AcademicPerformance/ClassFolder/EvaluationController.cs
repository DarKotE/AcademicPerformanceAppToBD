using System.Collections.Generic;

namespace AcademicPerformance.ClassFolder
{

    public class EvaluationController
    {
        public CDataAccess DataAccess { get; }

        public EvaluationController()
        {
            DataAccess = new CDataAccess();
        }

        public List<EvaluationModel> GetAll()
        {
            var evaluationList = DataAccess.GetEvaluationList();
            return evaluationList ?? new List<EvaluationModel>();
        }

        public bool Add(EvaluationModel newEvaluation)
        {
            return DataAccess != null && DataAccess.InsertEvaluation(newEvaluation);
        }

        public bool Update(EvaluationModel evaluationToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateEvaluation(evaluationToUpdate);
        }

        public bool Delete(int idEvaluation)
        {
            return DataAccess != null && DataAccess.DeleteEvaluation(idEvaluation);
        }

        public EvaluationModel SelectId(int idEvaluation)
        {
            return DataAccess != null ? DataAccess.GetEvaluation(idEvaluation) : new EvaluationModel();
        }

    }
}
