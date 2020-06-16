using System.Collections.Generic;

namespace AcademicPerformance.ClassFolder
{
    public class EvaluationController
    {
        public EvaluationController()
        {
            DataAccess = new CDataAccess();
        }

        public CDataAccess DataAccess { get; }

        public bool Add(EvaluationModel newEvaluation)
        {
            return DataAccess != null && DataAccess.InsertEvaluation(newEvaluation);
        }

        public bool Delete(int idEvaluation)
        {
            return DataAccess != null && DataAccess.DeleteEvaluation(idEvaluation);
        }

        public List<EvaluationModel> GetAll()
        {
            var evaluationList = DataAccess.GetEvaluationList();
            return evaluationList ?? new List<EvaluationModel>();
        }

        public EvaluationModel SelectId(int idEvaluation)
        {
            return DataAccess != null ? DataAccess.GetEvaluation(idEvaluation) : new EvaluationModel();
        }

        public bool Update(EvaluationModel evaluationToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateEvaluation(evaluationToUpdate);
        }
    }
}