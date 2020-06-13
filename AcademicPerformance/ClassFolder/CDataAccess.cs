using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using AcademicPerformance.ClassFolder;

namespace AcademicPerformance
{
    public class CDataAccess
    {
 
        public bool IsAuthValid(string userLogin, string userPassword)
        {
            
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                string sqlQuery = "SELECT IdUser FROM [dbo].[User] Where  LoginUser  = @LoginUser AND PasswordUser = @PasswordUser";
                SqlCommand sqlCommand;
                SqlDataReader sqlDataReader;
                sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("LoginUser", userLogin);
                sqlCommand.Parameters.AddWithValue("PasswordUser", userPassword);
                try
                {
                    sqlConnection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
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
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
                return false;

            }
                
        }
        public bool IsLoginFree(string login)
        {

            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                SqlCommand sqlCommand;
                SqlDataReader sqlDataReader;
                sqlCommand = new SqlCommand("select IdUser From dbo.[User] Where  LoginUser='" + login + "'", sqlConnection);
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
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


        public List<UserModel> GetUserList()
        {
            List<UserModel> tempUserList = new List<UserModel>();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdUser, LoginUser, PasswordUser, RoleUser";
                    sqlQuery += " FROM [dbo].[User]";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<UserModel> users = new List<UserModel>();
                        while (reader.Read())
                        {
                            UserModel u = new UserModel();
                            u.IdUser = reader.GetInt32(0);
                            u.LoginUser = reader.GetString(1);
                            u.PasswordUser = reader.GetString(2);
                            u.RoleUser = reader.GetInt32(3);
                            users.Add(u);
                        }
                        tempUserList = users;
                    }
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            UserModel tempUserModel = new UserModel();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdUser, LoginUser, PasswordUser, RoleUser";
                    sqlQuery += " FROM [dbo].[User]";
                    sqlQuery += " WHERE [User].LoginUser = @LoginUser AND [User].PasswordUser = @PasswordUser";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("LoginUser", userLogin);
                    sqlCommand.Parameters.AddWithValue("PasswordUser", userPassword);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    List<UserModel> users = new List<UserModel>();
                    while (reader.Read())
                    {
                        UserModel u = new UserModel();
                        u.IdUser = (int)reader["IdUser"];
                        u.LoginUser = (string)reader["LoginUser"];
                        u.PasswordUser = (string)reader["PasswordUser"];
                        u.RoleUser = (int)reader["RoleUser"];
                        users.Add(u);
                    }
                    tempUserModel = users[0];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            bool isInserted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
               
                try
                {
                    string sqlQuery = "INSERT INTO dbo.[User] (LoginUser,PasswordUser,RoleUser) VALUES (@LoginUser, @PasswordUser, @RoleUser)";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("LoginUser", userModel.LoginUser);
                    sqlCommand.Parameters.AddWithValue("PasswordUser", userModel.PasswordUser);
                    sqlCommand.Parameters.AddWithValue("RoleUser", userModel.RoleUser);
                    sqlConnection.Open();
                    int NoOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    isInserted = NoOfRowsAffected > 0;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            bool isUpdated = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {

                try
                {
                    string sqlQuery = "UPDATE dbo.[User] set LoginUser=@LoginUser, PasswordUser=@PasswordUser, RoleUser=@RoleUser WHERE IdUser=@IdUser";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("LoginUser", userModel.LoginUser);
                    sqlCommand.Parameters.AddWithValue("PasswordUser", userModel.PasswordUser);
                    sqlCommand.Parameters.AddWithValue("RoleUser", userModel.RoleUser);
                    sqlCommand.Parameters.AddWithValue("IdUser", userModel.IdUser);
                    sqlConnection.Open();
                    int NoOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    isUpdated = NoOfRowsAffected > 0;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            bool isDeleted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {

                try
                {
                    string sqlQuery = "DELETE FROM  dbo.[User] WHERE IdUser=@IdUser";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdUser", idUser);
                    sqlConnection.Open();
                    int NoOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    isDeleted = NoOfRowsAffected > 0;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdUser, LoginUser, PasswordUser, RoleUser";
                    sqlQuery += " FROM [dbo].[User] WHERE IdUser=@IdUser";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdUser", idUser);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows) 
                    {
                        reader.Read();
                        tempUser= new UserModel();
                        tempUser.IdUser = reader.GetInt32(0);
                        tempUser.LoginUser = reader.GetString(1);
                        tempUser.PasswordUser = reader.GetString(2);
                        tempUser.RoleUser = reader.GetInt32(3);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdUser, LoginUser, PasswordUser, RoleUser";
                    sqlQuery += " FROM [dbo].[User] WHERE LoginUser=@LoginUser";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("LoginUser", loginUser);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        tempUser = new UserModel();
                        tempUser.IdUser = reader.GetInt32(0);
                        tempUser.LoginUser = reader.GetString(1);
                        tempUser.PasswordUser = reader.GetString(2);
                        tempUser.RoleUser = reader.GetInt32(3);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    sqlConnection.Close();

                }
            }
            return tempUser;
        }

        public DataTable GetJournalTableVar()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {   
                string sqlQuery = "select [IdJournal],";
                       sqlQuery += " RTRIM(LTRIM(CONCAT(COALESCE([LastNameStudent] + ' ', ''), COALESCE([FirstNameStudent] + ' ', ''), COALESCE([MiddleNameStudent], '')))) AS FIOStudent,";
                       sqlQuery += " [NameEvaluation],[NumberEvaluation],";
                       sqlQuery += " RTRIM(LTRIM(CONCAT(COALESCE([LastNameTeacher] + ' ', ''), COALESCE([FirstNameTeacher] + ' ', ''), COALESCE([MiddleNameTeacher], '')))) AS FIOTeacher,";
                       sqlQuery += " [NameDiscipline] ";
                       sqlQuery += " FROM [dbo].[Journal]";
                       sqlQuery += " inner join [dbo].Student on Student.IdStudent = Journal.IdStudent";
                       sqlQuery += " inner join [dbo].Teacher on Teacher.IdTeacher = Journal.IdTeacher";
                       sqlQuery += " inner join [dbo].Evaluation on Evaluation.IdEvaluation = Journal.IdEvaluation";
                       sqlQuery += " inner join [dbo].Discipline on Discipline.IdDiscipline = Journal.IdDiscipline";
                       sqlQuery += " WHERE Student.IdUser = @IdUser";                   

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("IdUser", App.IdUser);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataTable); 
            }
            return dataTable;
        }

        public List<StudentJournalModel> GetJournalList()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                string sqlQuery = "select [IdJournal],";
                sqlQuery +=
                    " RTRIM(LTRIM(CONCAT(COALESCE([LastNameStudent] + ' ', ''), COALESCE([FirstNameStudent] + ' ', ''), COALESCE([MiddleNameStudent], '')))) AS FIOStudent,";
                sqlQuery += " [NameEvaluation],[NumberEvaluation],";
                sqlQuery +=
                    " RTRIM(LTRIM(CONCAT(COALESCE([LastNameTeacher] + ' ', ''), COALESCE([FirstNameTeacher] + ' ', ''), COALESCE([MiddleNameTeacher], '')))) AS FIOTeacher,";
                sqlQuery += " [NameDiscipline] ";
                sqlQuery += " FROM [dbo].[Journal]";
                sqlQuery += " inner join [dbo].Student on Student.IdStudent = Journal.IdStudent";
                sqlQuery += " inner join [dbo].Teacher on Teacher.IdTeacher = Journal.IdTeacher";
                sqlQuery += " inner join [dbo].Evaluation on Evaluation.IdEvaluation = Journal.IdEvaluation";
                sqlQuery += " inner join [dbo].Discipline on Discipline.IdDiscipline = Journal.IdDiscipline";
                sqlQuery += " WHERE Student.IdUser = @IdUser";

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("IdUser", App.IdUser);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataTable);

                List<StudentJournalModel> studentList = new List<StudentJournalModel>();
                studentList = (from DataRow dataRow in dataTable.Rows
                    select new StudentJournalModel()
                    {
                        IdJournal = Convert.ToInt32(dataRow["IdJournal"]),
                        FIOStudent = dataRow["FIOStudent"].ToString(),
                        FIOTeacher = dataRow["FIOTeacher"].ToString(),
                        NameDiscipline = dataRow["NameDiscipline"].ToString(),
                        NameEvaluation = dataRow["NameEvaluation"].ToString(),
                        NumberEvaluation = Convert.ToInt32(dataRow["NumberEvaluation"]),
                    }).ToList();

                var output = studentList;
                return output;
            }
        }

        //public List<CJournal> GetJournal(int idJournal, int idTeacher, int idDiscipline, int idEvaluation)
        //{
        //    using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
        //    {
        //        DataTable dataTable = new DataTable();
        //        string sqlQuery = "SELECT IdJournal, IdTeacher, IdDiscipline, IdEvaluation FROM [dbo].[Journal]";
        //        SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
        //        dataAdapter.Fill(dataTable);
        //        List<Student> studentList = new List<Student>();
        //        studentList = (from DataRow dr in dt.Rows
        //                       select new Student()
        //                       {
        //                           StudentId = Convert.ToInt32(dr["StudentId"]),
        //                           StudentName = dr["StudentName"].ToString(),
        //                           Address = dr["Address"].ToString(),
        //                           MobileNo = dr["MobileNo"].ToString()
        //                       }).ToList();

        //    }
        //}

    }
}
