using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.Classes.DataSqlGateways
{
    public partial class SqlDataAccess
    {
        #region TeacherAccess

        public List<TeacherModel> GetTeacherList()
        {
            var tempTeacherList = new List<TeacherModel>();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery =
                        @"SELECT IdTeacher, LastNameTeacher,FirstNameTeacher, 
                        MiddleNameTeacher,DateOfBirthTeacher,NumberPhoneTeacher, IdUser
                        FROM [dbo].[Teacher]";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        sqlConnection.Open();
                        reader = sqlCommand.ExecuteReader();
                    }

                    if (reader.HasRows)
                    {
                        var items = new List<TeacherModel>();
                        while (reader.Read())
                        {
                            var u = new TeacherModel
                            {
                                IdTeacher = reader.GetInt32(0),
                                LastNameTeacher = reader.GetString(1),
                                FirstNameTeacher = reader.GetString(2),
                                MiddleNameTeacher = reader.GetString(3),
                                DateOfBirthTeacher = reader.GetDateTime(4),
                                NumberPhoneTeacher = reader.GetString(5),
                                IdUser = reader.GetInt32(6)
                            };

                            items.Add(u);
                        }

                        tempTeacherList = items;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return tempTeacherList;
        }

        public TeacherModel GetTeacher(int idUser)
        {
            var tempTeacherModel = new TeacherModel();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = 
                        @"SELECT IdTeacher, LastNameTeacher, FirstNameTeacher, MiddleNameTeacher, 
                        DateOfBirthTeacher, NumberPhoneTeacher,IdUser
                        FROM [dbo].[Teacher]
                        WHERE [Teacher].IdUser = @IdUser";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("IdUser", idUser);
                        reader = sqlCommand.ExecuteReader();
                    }

                    var items = new List<TeacherModel>();
                    while (reader.Read())
                    {
                        var u = new TeacherModel
                        {
                            IdTeacher = (int) reader["IdTeacher"],
                            LastNameTeacher = (string) reader["LastNameTeacher"],
                            FirstNameTeacher = (string) reader["FirstNameTeacher"],
                            MiddleNameTeacher = (string) reader["MiddleNameTeacher"],
                            DateOfBirthTeacher = (DateTime) reader["DateOfBirthTeacher"],
                            NumberPhoneTeacher = (string) reader["NumberPhoneTeacher"],
                            IdUser = (int) reader["IdUser"]
                        };
                        items.Add(u);
                    }

                    if (items.Count > 0) tempTeacherModel = items[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return tempTeacherModel;
        }

        public bool InsertTeacher(TeacherModel teacherModel)
        {
            var isInserted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = 
                        @"INSERT INTO dbo.[Teacher] 
                        (IdUser,LastNameTeacher,FirstNameTeacher, MiddleNameTeacher,
                        DateOfBirthTeacher,NumberPhoneTeacher)
                        VALUES (@IdUser, @LastNameTeacher, @FirstNameTeacher,@MiddleNameTeacher,
                        @DateOfBirthTeacher,@NumberPhoneTeacher)";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        if (teacherModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("IdUser", teacherModel.IdUser);
                            sqlCommand.Parameters.AddWithValue("LastNameTeacher", teacherModel.LastNameTeacher);
                            sqlCommand.Parameters.AddWithValue("FirstNameTeacher", teacherModel.FirstNameTeacher);
                            sqlCommand.Parameters.AddWithValue("MiddleNameTeacher", teacherModel.MiddleNameTeacher);
                            sqlCommand.Parameters.AddWithValue("DateOfBirthTeacher", teacherModel.DateOfBirthTeacher);
                            sqlCommand.Parameters.AddWithValue("NumberPhoneTeacher", teacherModel.NumberPhoneTeacher);
                        }

                        sqlConnection.Open();
                        noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    }

                    isInserted = noOfRowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return isInserted;
        }

        public bool UpdateTeacher(TeacherModel teacherModel)
        {
            var isUpdated = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery =
                        @"UPDATE dbo.[Teacher] 
                        SET IdUser=@IdUser, LastNameTeacher=@PasswordUser, 
                        FirstNameTeacher=@FirstNameTeacher, DateOfBirthTeacher=@DateOfBirthTeacher,
                        NumberPhoneTeacher=@NumberPhoneTeacher 
                        WHERE IdTeacher=@IdTeacher";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        if (teacherModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("IdUser",
                                teacherModel.IdUser);
                            sqlCommand.Parameters.AddWithValue("LastNameTeacher",
                                teacherModel.LastNameTeacher);
                            sqlCommand.Parameters.AddWithValue("FirstNameTeacher",
                                teacherModel.FirstNameTeacher);
                            sqlCommand.Parameters.AddWithValue("MiddleNameTeacher",
                                teacherModel.MiddleNameTeacher);
                            sqlCommand.Parameters.AddWithValue("DateOfBirthTeacher",
                                teacherModel.DateOfBirthTeacher);
                            sqlCommand.Parameters.AddWithValue("NumberPhoneTeacher",
                                teacherModel.NumberPhoneTeacher);
                            sqlCommand.Parameters.AddWithValue("IdTeacher",
                                teacherModel.IdTeacher);
                        }

                        sqlConnection.Open();
                        noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    }

                    isUpdated = noOfRowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return isUpdated;
        }

        public bool DeleteTeacher(int idUser)
        {
            var isDeleted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = "DELETE FROM  dbo.[Teacher] WHERE IdUser=@IdUser";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("IdUser", idUser);
                        sqlConnection.Open();
                        noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    }

                    isDeleted = noOfRowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
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