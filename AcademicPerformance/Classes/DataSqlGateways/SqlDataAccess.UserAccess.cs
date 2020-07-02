using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.Classes.DataSqlGateways
{
    public partial class SqlDataAccess
    {
        #region AuthLoginValidation

        public bool IsAuthValid(string userLogin, string userPassword)
        {
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                const string sqlQuery = "SELECT IdUser FROM [dbo].[User] " +
                                        "Where  LoginUser  = @LoginUser AND PasswordUser = @PasswordUser";
                using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("LoginUser", userLogin);
                    sqlCommand.Parameters.AddWithValue("PasswordUser", userPassword);
                    try
                    {
                        sqlConnection.Open();
                        var sqlDataReader = sqlCommand.ExecuteReader();
                        if (sqlDataReader.Read())
                        {
                            sqlDataReader.Close();
                            return true;
                        }
                        else
                        {
                            sqlDataReader.Close();
                            return false;
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

                return false;
            }
        }

        public bool IsLoginFree(string login)
        {
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                SqlDataReader sqlDataReader;
                string sqlString = "select IdUser From dbo.[User]"
                                   + " Where  LoginUser=@LoginUser";
                using (var sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("LoginUser", login);
                    sqlConnection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                }

                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Close();
                    return false;
                }
                else
                {
                    sqlDataReader.Close();
                    return true;
                }
            }
        }

        #endregion

        #region UserAccess

        public List<UserModel> GetUserList()
        {
            var tempUserList = new List<UserModel>();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "SELECT IdUser, LoginUser, PasswordUser, RoleUser";
                    sqlQuery += " FROM [dbo].[User]";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        sqlConnection.Open();
                        reader = sqlCommand.ExecuteReader();
                    }

                    if (reader.HasRows)
                    {
                        var users = new List<UserModel>();
                        while (reader.Read())
                        {
                            var u = new UserModel
                            {
                                IdUser = reader.GetInt32(0),
                                LoginUser = reader.GetString(1),
                                PasswordUser = reader.GetString(2),
                                RoleUser = reader.GetInt32(3)
                            };
                            users.Add(u);
                        }

                        tempUserList = users;
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

            return tempUserList;
        }

        public UserModel GetUser(string userLogin, string userPassword)
        {
            var tempUserModel = new UserModel();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "SELECT IdUser, LoginUser, PasswordUser, RoleUser";
                    sqlQuery += " FROM [dbo].[User]";
                    sqlQuery += " WHERE [User].LoginUser = @LoginUser " +
                                "AND [User].PasswordUser = @PasswordUser";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("LoginUser", userLogin);
                        sqlCommand.Parameters.AddWithValue("PasswordUser", userPassword);
                        reader = sqlCommand.ExecuteReader();
                    }

                    var items = new List<UserModel>();
                    while (reader.Read())
                    {
                        var u = new UserModel
                        {
                            IdUser = (int) reader["IdUser"],
                            LoginUser = (string) reader["LoginUser"],
                            PasswordUser = (string) reader["PasswordUser"],
                            RoleUser = (int) reader["RoleUser"]
                        };
                        items.Add(u);
                    }

                    if (items.Count > 0) tempUserModel = items[0];
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

            return tempUserModel;
        }

        public bool InsertUser(UserModel userModel)
        {
            var isInserted = false;
            using (var sqlConnection =
                new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "INSERT INTO dbo.[User] (LoginUser,PasswordUser,"
                                   + "RoleUser) VALUES (@LoginUser, @PasswordUser, @RoleUser)";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        if (userModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("LoginUser", userModel.LoginUser);
                            sqlCommand.Parameters.AddWithValue("PasswordUser", userModel.PasswordUser);
                            sqlCommand.Parameters.AddWithValue("RoleUser", userModel.RoleUser);
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

        public bool UpdateUser(UserModel userModel)
        {
            var isUpdated = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "UPDATE dbo.[User] set LoginUser=@LoginUser, "
                                   + "PasswordUser=@PasswordUser, RoleUser=@RoleUser WHERE IdUser=@IdUser";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        if (userModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("LoginUser", userModel.LoginUser);
                            sqlCommand.Parameters.AddWithValue("PasswordUser", userModel.PasswordUser);
                            sqlCommand.Parameters.AddWithValue("RoleUser", userModel.RoleUser);
                            sqlCommand.Parameters.AddWithValue("IdUser", userModel.IdUser);
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

        public bool DeleteUser(int idUser)
        {
            var isDeleted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    const string sqlQuery = "DELETE FROM  dbo.[User]" +
                                            " WHERE IdUser=@IdUser";
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

        public UserModel SelectUserId(int idUser)
        {
            UserModel tempUser = null;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "SELECT IdUser, LoginUser, PasswordUser, RoleUser";
                    sqlQuery += " FROM [dbo].[User] WHERE IdUser=@IdUser";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("IdUser", idUser);
                        sqlConnection.Open();
                        reader = sqlCommand.ExecuteReader();
                    }

                    if (reader.HasRows)
                    {
                        reader.Read();
                        tempUser = new UserModel
                        {
                            IdUser = reader.GetInt32(0),
                            LoginUser = reader.GetString(1),
                            PasswordUser = reader.GetString(2),
                            RoleUser = reader.GetInt32(3)
                        };
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

            return tempUser;
        }

        public UserModel SelectUserLogin(string loginUser)
        {
            UserModel tempUser = null;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "SELECT IdUser, LoginUser, PasswordUser, RoleUser";
                    sqlQuery += " FROM [dbo].[User] WHERE LoginUser=@LoginUser";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("LoginUser", loginUser);
                        sqlConnection.Open();
                        reader = sqlCommand.ExecuteReader();
                    }

                    if (reader.HasRows)
                    {
                        reader.Read();
                        tempUser = new UserModel
                        {
                            IdUser = reader.GetInt32(0),
                            LoginUser = reader.GetString(1),
                            PasswordUser = reader.GetString(2),
                            RoleUser = reader.GetInt32(3)
                        };
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

            return tempUser;
        }

        #endregion
    }
}