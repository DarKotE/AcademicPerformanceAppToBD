using System.Collections.Generic;
using AcademicPerformance.Classes.DataModels;
using AcademicPerformance.Classes.DataSqlGateways;

namespace AcademicPerformance.Classes.DataAdapters
{
    public class EvaluationAdapter
    {
        public EvaluationAdapter()
        {
            DataAccess = new SqlDataAccess();
        }

        public SqlDataAccess DataAccess { get; }

        public bool AddEvaluation(EvaluationModel newEvaluation)
        {
            return DataAccess != null && DataAccess.InsertEvaluation(newEvaluation);
        }

        public bool DeleteEvaluationById(int idEvaluation)
        {
            return DataAccess != null && DataAccess.DeleteEvaluation(idEvaluation);
        }

        public List<EvaluationModel> GetAllEvaluation()
        {
            var evaluationList = DataAccess.GetEvaluationList();
            return evaluationList ?? new List<EvaluationModel>();
        }

        public EvaluationModel GetEvaluationById(int idEvaluation)
        {
            return DataAccess != null ? 
                DataAccess.GetEvaluation(idEvaluation) : new EvaluationModel();
        }

        public bool SetEvaluation(EvaluationModel evaluationToUpdate)
        {
            return DataAccess != null && 
                   DataAccess.UpdateEvaluation(evaluationToUpdate);
        }
    }
}