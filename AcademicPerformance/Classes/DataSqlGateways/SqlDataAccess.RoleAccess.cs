using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using AcademicPerformance.Classes.DataModels;

namespace AcademicPerformance.Classes.DataSqlGateways
{
    public partial class SqlDataAccess
    {
        #region RoleAccess

        public List<RoleModel> GetRoleList()
        {
            var tempRoleList = new List<RoleModel>();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "SELECT IdRole, NameRole";
                    sqlQuery += " FROM [dbo].[Role]";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlConnection.Open();
                        reader = sqlCommand.ExecuteReader();
                    }

                    if (reader.HasRows)
                    {
                        var roles = new List<RoleModel>();
                        while (reader.Read())
                        {
                            var u = new RoleModel
                            {
                                IdRole = reader.GetInt32(0),
                                NameRole = reader.GetString(1)
                            };
                            roles.Add(u);
                        }

                        tempRoleList = roles;
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

            return tempRoleList;
        }

        public RoleModel GetRole(int idRole)
        {
            var tempRole = new RoleModel();
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "SELECT IdRole, NameRole";
                    sqlQuery += " FROM [dbo].[Role]";
                    sqlQuery += " WHERE [Role].IdRole = @IdRole";
                    SqlDataReader reader;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("IdRole",
                            idRole);

                        reader = sqlCommand.ExecuteReader();
                    }

                    var items = new List<RoleModel>();
                    while (reader.Read())
                    {
                        var u = new RoleModel
                        {
                            IdRole = (int) reader["IdRole"],
                            NameRole = (string) reader["NameRole"]
                        };
                        items.Add(u);
                    }

                    if (items.Count > 0)
                        tempRole = items[0];
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

            return tempRole;
        }

        public bool InsertRole(RoleModel roleModel)
        {
            var isInserted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "INSERT INTO dbo.[Role] (IdRole,NameRole)" + " VALUES (@IdRole, @NameRole)";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (roleModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("IdRole",
                                roleModel.IdRole);
                            sqlCommand.Parameters.AddWithValue("NameRole",
                                roleModel.NameRole);
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

        public bool UpdateRole(RoleModel roleModel)
        {
            var isUpdated = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "UPDATE dbo.[Role] set NameRole=@NameRole " + "WHERE IdRole=@IdRole";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        if (roleModel != null)
                        {
                            sqlCommand.Parameters.AddWithValue("NameRole",
                                roleModel.NameRole);
                            sqlCommand.Parameters.AddWithValue("IdRole",
                                roleModel.IdRole);
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

        public bool DeleteRole(int idRole)
        {
            var isDeleted = false;
            using (var sqlConnection = new SqlConnection(ConstSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    var sqlQuery = "DELETE FROM  dbo.[Role] WHERE IdRole=@IdRole";
                    int noOfRowsAffected;
                    using (var sqlCommand = new SqlCommand(sqlQuery,
                        sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("IdRole",
                            idRole);
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