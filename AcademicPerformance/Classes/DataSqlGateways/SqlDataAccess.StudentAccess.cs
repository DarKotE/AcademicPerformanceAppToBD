using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.Classes.DataSqlGateways
{
    public partial class SqlDataAccess
    {
        #region StudentAccess

        public List<StudentModel> GetStudentList()
        {
            var tempStudentList = new List<StudentModel>();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "SELECT IdStudent, LastNameStudent, " +
                                   "FirstNameStudent,MiddleNameStudent,DateOfBirthStudent,NumberPhoneStudent,IdUser";
                    sqlQuery += " FROM [dbo].[Student]";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlConnection.Open();
                        reader = sqlCommand.ExecuteReader();
                    }

                    if (reader.HasRows)
                    {
                        var items = new List<StudentModel>();
                        while (reader.Read())
                        {
                            var u = new StudentModel
                            {
                                IdStudent = reader.GetInt32(0),
                                LastNameStudent = reader.GetString(1),
                                FirstNameStudent = reader.GetString(2),
                                MiddleNameStudent = reader.GetString(3),
                                DateOfBirthStudent = reader.GetDateTime(4),
                                NumberPhoneStudent = reader.GetString(5),
                                IdUser = reader.GetInt32(6)
                            };
                            items.Add(u);
                        }

                        tempStudentList = items;
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

            return tempStudentList;
        }

        public StudentModel GetStudent(int idUser)
        {
            var tempStudentModel = new StudentModel();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "SELECT IdUser, IdStudent, LastNameStudent, " +
                                   "FirstNameStudent, MiddleNameStudent, DateOfBirthStudent, NumberPhoneStudent";
                    sqlQuery += " FROM [dbo].[Student]";
                    sqlQuery += " WHERE [Student].IdUser = @IdUser";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("IdUser",
                            idUser);
                        reader = sqlCommand.ExecuteReader();
                    }

                    var items = new List<StudentModel>();
                    while (reader.Read())
                    {
                        var u = new StudentModel
                        {
                            IdStudent = (int) reader["IdStudent"],
                            IdUser = (int) reader["IdUser"],
                            LastNameStudent = (string) reader["LastNameStudent"],
                            FirstNameStudent = (string) reader["FirstNameStudent"],
                            MiddleNameStudent = (string) reader["MiddleNameStudent"],
                            DateOfBirthStudent = (DateTime) reader["DateOfBirthStudent"],
                            NumberPhoneStudent = (string) reader["NumberPhoneStudent"]
                        };
                        items.Add(u);
                    }

                    if (items.Count > 0)
                        tempStudentModel = items[0];
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

            return tempStudentModel;
        }

        public bool InsertStudent(StudentModel studentModel)
        {
            var isInserted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = "INSERT INTO dbo.[Student] (IdUser,LastNameStudent, " +
                                            "FirstNameStudent, MiddleNameStudent, DateOfBirthStudent, NumberPhoneStudent)" +
                                            " VALUES (@IdUser, @LastNameStudent, @FirstNameStudent, @MiddleNameStudent, @DateOfBirthStudent, @NumberPhoneStudent)";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (studentModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("IdUser",
                                studentModel.IdUser);
                            sqlCommand.Parameters.AddWithValue("LastNameStudent",
                                studentModel.LastNameStudent);
                            sqlCommand.Parameters.AddWithValue("FirstNameStudent",
                                studentModel.FirstNameStudent);
                            sqlCommand.Parameters.AddWithValue("MiddleNameStudent",
                                studentModel.MiddleNameStudent);
                            sqlCommand.Parameters.AddWithValue("DateOfBirthStudent",
                                studentModel.DateOfBirthStudent);
                            sqlCommand.Parameters.AddWithValue("NumberPhoneStudent",
                                studentModel.NumberPhoneStudent);
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

        public bool UpdateStudent(StudentModel studentModel)
        {
            var isUpdated = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = "UPDATE dbo.[Student] set IdUser=@IdUser, " +
                                            "LastNameStudent=@LastNameStudent, FirstNameStudent=@FirstNameStudent,MiddleNameStudent=@MiddleNameStudent, DateOfBirthStudent=@DateOfBirthStudent, NumberPhoneStudent=@NumberPhoneStudent WHERE IdStudent=@IdStudent";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (studentModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("IdUser",
                                studentModel.IdUser);
                            sqlCommand.Parameters.AddWithValue("LastNameStudent",
                                studentModel.LastNameStudent);
                            sqlCommand.Parameters.AddWithValue("FirstNameStudent",
                                studentModel.FirstNameStudent);
                            sqlCommand.Parameters.AddWithValue("MiddleNameStudent",
                                studentModel.MiddleNameStudent);
                            sqlCommand.Parameters.AddWithValue("DateOfBirthStudent",
                                studentModel.DateOfBirthStudent);
                            sqlCommand.Parameters.AddWithValue("NumberPhoneStudent",
                                studentModel.NumberPhoneStudent);
                            sqlCommand.Parameters.AddWithValue("IdStudent",
                                studentModel.IdStudent);
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

        public bool DeleteStudent(int idUser)
        {
            var isDeleted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = "DELETE FROM  dbo.[Student] WHERE IdUser=@IdUser";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("IdUser",
                            idUser);
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