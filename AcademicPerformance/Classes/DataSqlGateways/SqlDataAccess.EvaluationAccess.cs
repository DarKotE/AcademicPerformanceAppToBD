using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.Classes.DataSqlGateways
{
    public partial class SqlDataAccess
    {
        #region EvaluationAccess

        public List<EvaluationModel> GetEvaluationList()
        {
            var tempEvaluationList = new List<EvaluationModel>();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = 
                        @"SELECT IdEvaluation, NameEvaluation, NumberEvaluation
                        FROM [dbo].[Evaluation]";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlConnection.Open();
                        reader = sqlCommand.ExecuteReader();
                    }

                    if (reader.HasRows)
                    {
                        var items = new List<EvaluationModel>();
                        while (reader.Read())
                        {
                            var u = new EvaluationModel
                            {
                                IdEvaluation = reader.GetInt32(0),
                                NameEvaluation = reader.GetString(1),
                                NumberEvaluation = reader.GetInt32(2)
                            };
                            items.Add(u);
                        }

                        tempEvaluationList = items;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return tempEvaluationList;
        }

        public EvaluationModel GetEvaluation(int idEvaluation)
        {
            var tempEvaluation = new EvaluationModel();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery =
                        @"SELECT IdEvaluation, NameEvaluation, NumberEvaluation
                        FROM [dbo].[Evaluation]
                        WHERE [Evaluation].IdEvaluation = @IdEvaluation";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("IdEvaluation",
                            idEvaluation);
                        reader = sqlCommand.ExecuteReader();
                    }

                    var items = new List<EvaluationModel>();
                    while (reader.Read())
                    {
                        var u = new EvaluationModel
                        {
                            IdEvaluation = (int) reader["IdEvaluation"],
                            NameEvaluation = (string) reader["NameEvaluation"],
                            NumberEvaluation = (int) reader["NumberEvaluation"]
                        };

                        items.Add(u);
                    }

                    if (items.Count > 0)
                        tempEvaluation = items[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return tempEvaluation;
        }

        public bool InsertEvaluation(EvaluationModel evaluationModel)
        {
            var isInserted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = 
                        @"INSERT INTO dbo.[Evaluation] 
                        (IdEvaluation, NameEvaluation, NumberEvaluation) 
                        VALUES (@IdEvaluation, @NameEvaluation,@NumberEvaluation)";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (evaluationModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("IdDiscipline",
                                evaluationModel.IdEvaluation);
                            sqlCommand.Parameters.AddWithValue("NameEvaluation",
                                evaluationModel.NameEvaluation);
                            sqlCommand.Parameters.AddWithValue("NumberEvaluation",
                                evaluationModel.NumberEvaluation);
                        }

                        sqlConnection.Open();
                        noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    }

                    isInserted = noOfRowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return isInserted;
        }

        public bool UpdateEvaluation(EvaluationModel evaluationModel)
        {
            var isUpdated = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery =
                        @"UPDATE dbo.[Evaluation] 
                        SET NameEvaluation=@NameEvaluation,NumberEvaluation=@NumberEvaluation
                        WHERE IdEvaluation=@IdEvaluation";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (evaluationModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("NameEvaluation",
                                evaluationModel.NameEvaluation);
                            sqlCommand.Parameters.AddWithValue("NumberEvaluation",
                                evaluationModel.NumberEvaluation);
                            sqlCommand.Parameters.AddWithValue("IdEvaluation",
                                evaluationModel.IdEvaluation);
                        }

                        sqlConnection.Open();
                        noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    }

                    isUpdated = noOfRowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return isUpdated;
        }

        public bool DeleteEvaluation(int idEvaluation)
        {
            var isDeleted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = 
                        @"DELETE FROM  dbo.[Evaluation] WHERE IdEvaluation=@IdEvaluation";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("IdEvaluation",
                            idEvaluation);
                        sqlConnection.Open();
                        noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    }

                    isDeleted = noOfRowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return isDeleted;
        }

        #endregion
    }

}