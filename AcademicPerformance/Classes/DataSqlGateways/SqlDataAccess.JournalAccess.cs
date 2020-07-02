using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.Classes.DataSqlGateways
{
    public partial class SqlDataAccess
    {
        #region JournalAccess

        public List<JournalModel> GetJournalListForUser()
        {
            var dataTable = new DataTable();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                var sqlQuery = "select [IdJournal],";
                sqlQuery +=
                    " RTRIM(LTRIM(CONCAT(COALESCE([LastNameStudent] + ' ', '')," +
                    " COALESCE([FirstNameStudent] + ' ', ''), COALESCE([MiddleNameStudent], '')))) AS FIOStudent,";
                sqlQuery += " [NameEvaluation],[NumberEvaluation],";
                sqlQuery +=
                    " RTRIM(LTRIM(CONCAT(COALESCE([LastNameTeacher] + ' ', ''), " +
                    "COALESCE([FirstNameTeacher] + ' ', ''), COALESCE([MiddleNameTeacher]," +
                    " '')))) AS FIOTeacher,";
                sqlQuery += " [NameDiscipline], [Journal].[IdStudent], [Journal].[IdTeacher]," +
                            " [Journal].[IdDiscipline], [Journal].[IdEvaluation]";
                sqlQuery += " FROM [dbo].[Journal]";
                sqlQuery += " inner join [dbo].Student on Student.IdStudent = Journal.IdStudent";
                sqlQuery += " inner join [dbo].Teacher on Teacher.IdTeacher = Journal.IdTeacher";
                sqlQuery += " inner join [dbo].Evaluation on Evaluation.IdEvaluation = Journal.IdEvaluation";
                sqlQuery += " inner join [dbo].Discipline on Discipline.IdDiscipline = Journal.IdDiscipline";
                sqlQuery += " WHERE Student.IdUser = @IdUser or Teacher.IdUser=@IdUser";

                SqlDataAdapter dataAdapter;
                using (var sqlCommand = new SqlCommand(sqlQuery,
                    sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("IdUser",
                        App.IdUser);
                    dataAdapter = new SqlDataAdapter(sqlCommand);
                }

                dataAdapter.Fill(dataTable);

                var journalList = (from DataRow dataRow in dataTable.Rows
                    select new JournalModel()
                    {
                        IdJournal = Convert.ToInt32(dataRow["IdJournal"]),
                        FIOStudent = dataRow["FIOStudent"]
                            .ToString(),
                        FIOTeacher = dataRow["FIOTeacher"]
                            .ToString(),
                        NameDiscipline = dataRow["NameDiscipline"]
                            .ToString(),
                        NameEvaluation = dataRow["NameEvaluation"]
                            .ToString(),
                        NumberEvaluation = Convert.ToInt32(dataRow["NumberEvaluation"]),
                        IdStudent = Convert.ToInt32(dataRow["IdStudent"]),
                        IdTeacher = Convert.ToInt32(dataRow["IdTeacher"]),
                        IdDiscipline = Convert.ToInt32(dataRow["IdDiscipline"]),
                        IdEvaluation = Convert.ToInt32(dataRow["IdEvaluation"]),
                    }).ToList();

                var output = journalList;
                return output;
            }
        }

        public List<JournalModel> GetJournalList()
        {
            var dataTable = new DataTable();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                var sqlQuery = "select [IdJournal],";
                sqlQuery +=
                    " RTRIM(LTRIM(CONCAT(COALESCE([LastNameStudent] + ' ', '')," +
                    " COALESCE([FirstNameStudent] + ' ', ''), COALESCE([MiddleNameStudent], '')))) AS FIOStudent,";
                sqlQuery += " [NameEvaluation],[NumberEvaluation],";
                sqlQuery +=
                    " RTRIM(LTRIM(CONCAT(COALESCE([LastNameTeacher] + ' ', ''), " +
                    "COALESCE([FirstNameTeacher] + ' ', ''), COALESCE([MiddleNameTeacher]," +
                    " '')))) AS FIOTeacher,";
                sqlQuery += " [NameDiscipline], [Journal].[IdStudent], [Journal].[IdTeacher]," +
                            " [Journal].[IdDiscipline], [Journal].[IdEvaluation]";
                sqlQuery += " FROM [dbo].[Journal]";
                sqlQuery += " inner join [dbo].Student on Student.IdStudent = Journal.IdStudent";
                sqlQuery += " inner join [dbo].Teacher on Teacher.IdTeacher = Journal.IdTeacher";
                sqlQuery += " inner join [dbo].Evaluation on Evaluation.IdEvaluation = Journal.IdEvaluation";
                sqlQuery += " inner join [dbo].Discipline on Discipline.IdDiscipline = Journal.IdDiscipline";

                SqlDataAdapter dataAdapter;
                using (var sqlCommand = new SqlCommand(sqlQuery,
                    sqlConnection))
                {
                    dataAdapter = new SqlDataAdapter(sqlCommand);
                }

                dataAdapter.Fill(dataTable);

                var journalList = (from DataRow dataRow in dataTable.Rows
                    select new JournalModel()
                    {
                        IdJournal = Convert.ToInt32(dataRow["IdJournal"]),
                        FIOStudent = dataRow["FIOStudent"]
                            .ToString(),
                        FIOTeacher = dataRow["FIOTeacher"]
                            .ToString(),
                        NameDiscipline = dataRow["NameDiscipline"]
                            .ToString(),
                        NameEvaluation = dataRow["NameEvaluation"]
                            .ToString(),
                        NumberEvaluation = Convert.ToInt32(dataRow["NumberEvaluation"]),
                        IdStudent = Convert.ToInt32(dataRow["IdStudent"]),
                        IdTeacher = Convert.ToInt32(dataRow["IdTeacher"]),
                        IdDiscipline = Convert.ToInt32(dataRow["IdDiscipline"]),
                        IdEvaluation = Convert.ToInt32(dataRow["IdEvaluation"]),
                    }).ToList();

                var output = journalList;
                return output;
            }
        }

        public JournalModel GetJournal(int idJournal)
        {
            var tempJournal = new JournalModel();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "SELECT IdJournal, IdStudent, IdTeacher, IdDiscipline, IdEvaluation";
                    sqlQuery += " FROM [dbo].[Journal]";
                    sqlQuery += " WHERE [Journal].IdJournal = @IdJournal";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("IdJournal",
                            idJournal);
                        reader = sqlCommand.ExecuteReader();
                    }

                    var items = new List<JournalModel>();
                    while (reader.Read())
                    {
                        var u = new JournalModel
                        {
                            IdJournal = (int) reader["IdEvaluation"],
                            IdStudent = (int) reader["IdStudent"],
                            IdTeacher = (int) reader["IdTeacher"],
                            IdDiscipline = (int) reader["IdDiscipline"],
                            IdEvaluation = (int) reader["IdEvaluation"]
                        };

                        items.Add(u);
                    }

                    if (items.Count > 0)
                        tempJournal = items[0];
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

            return tempJournal;
        }

        public bool InsertJournal(JournalModel journalModel)
        {
            var isInserted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "INSERT INTO dbo.[Journal] (IdStudent, IdTeacher, " +
                                   "IdDiscipline, IdEvaluation) VALUES (@IdStudent, " +
                                   "@IdTeacher, @IdDiscipline, @IdEvaluation)";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (journalModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("IdStudent",
                                journalModel.IdStudent);
                            sqlCommand.Parameters.AddWithValue("IdTeacher",
                                journalModel.IdTeacher);
                            sqlCommand.Parameters.AddWithValue("IdDiscipline",
                                journalModel.IdDiscipline);
                            sqlCommand.Parameters.AddWithValue("IdEvaluation",
                                journalModel.IdEvaluation);
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

        public bool UpdateJournal(JournalModel journalModel)
        {
            var isUpdated = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "UPDATE dbo.[Journal] set IdTeacher=@IdTeacher," +
                                   "IdStudent=@IdStudent,IdDiscipline=@IdDiscipline,IdEvaluation=@IdEvaluation" +
                                   " WHERE IdJournal=@IdJournal";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (journalModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("IdJournal",
                                journalModel.IdJournal);
                            sqlCommand.Parameters.AddWithValue("IdTeacher",
                                journalModel.IdTeacher);
                            sqlCommand.Parameters.AddWithValue("IdStudent",
                                journalModel.IdStudent);
                            sqlCommand.Parameters.AddWithValue("IdDiscipline",
                                journalModel.IdDiscipline);
                            sqlCommand.Parameters.AddWithValue("IdEvaluation",
                                journalModel.IdEvaluation);
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

        public bool DeleteJournal(int idJournal)
        {
            var isDeleted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "DELETE FROM  dbo.[Journal] WHERE idJournal=@idJournal";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("idJournal",
                            idJournal);
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