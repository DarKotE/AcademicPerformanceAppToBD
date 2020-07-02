using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.Classes.DataSqlGateways
{
    public partial class SqlDataAccess
    {
        #region DisciplineAccess

        public List<DisciplineModel> GetDisciplineList()
        {
            var tempDisciplineList = new List<DisciplineModel>();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = 
                        @"SELECT IdDiscipline, NameDiscipline
                        FROM [dbo].[Discipline]";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlConnection.Open();
                        reader = sqlCommand.ExecuteReader();
                    }

                    if (reader.HasRows)
                    {
                        var disciplines = new List<DisciplineModel>();
                        while (reader.Read())
                        {
                            var u = new DisciplineModel
                            {
                                IdDiscipline = reader.GetInt32(0),
                                NameDiscipline = reader.GetString(1)
                            };
                            disciplines.Add(u);
                        }

                        tempDisciplineList = disciplines;
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

            return tempDisciplineList;
        }

        public DisciplineModel GetDiscipline(int idDiscipline)
        {
            var tempDiscipline = new DisciplineModel();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = 
                        @"SELECT IdDiscipline, NameDiscipline
                        FROM [dbo].[Discipline]
                        WHERE [Discipline].IdDiscipline = @IdDiscipline";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("IdDiscipline",
                            idDiscipline);
                        reader = sqlCommand.ExecuteReader();
                    }

                    var items = new List<DisciplineModel>();
                    while (reader.Read())
                    {
                        var u = new DisciplineModel
                        {
                            IdDiscipline = (int) reader["IdDiscipline"],
                            NameDiscipline = (string) reader["NameDiscipline"]
                        };
                        items.Add(u);
                    }

                    if (items.Count > 0)
                        tempDiscipline = items[0];
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

            return tempDiscipline;
        }

        public bool InsertDiscipline(DisciplineModel disciplineModel)
        {
            var isInserted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = 
                        @"INSERT INTO dbo.[Discipline] (NameDiscipline)
                        VALUES (@NameDiscipline)";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (disciplineModel != null)
                            sqlCommand.Parameters.AddWithValue("NameDiscipline",
                                disciplineModel.NameDiscipline);
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

        public bool UpdateDiscipline(DisciplineModel disciplineModel)
        {
            var isUpdated = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = 
                        @"UPDATE dbo.[Discipline] set NameDiscipline=@NameDiscipline
                        WHERE IdDiscipline=@IdDiscipline";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (disciplineModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("NameDiscipline",
                                disciplineModel.NameDiscipline);
                            sqlCommand.Parameters.AddWithValue("IdDiscipline",
                                disciplineModel.IdDiscipline);
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

        public bool DeleteDiscipline(int idDiscipline)
        {
            var isDeleted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = 
                        @"DELETE FROM  dbo.[Discipline] WHERE IdDiscipline=@IdDiscipline";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("IdDiscipline",
                            idDiscipline);
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