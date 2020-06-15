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
        #region AuthLoginValidation
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
        #endregion  

        
        #region UserAccess
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
                    List<UserModel> items = new List<UserModel>();
                    while (reader.Read())
                    {
                        UserModel u = new UserModel();
                        u.IdUser = (int)reader["IdUser"];
                        u.LoginUser = (string)reader["LoginUser"];
                        u.PasswordUser = (string)reader["PasswordUser"];
                        u.RoleUser = (int)reader["RoleUser"];
                        items.Add(u);
                    }
                    if (items.Count>0)tempUserModel = items[0];

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

        #endregion

        
        #region JournalAccess
        
        public List<JournalModel> GetJournalList()
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
                sqlQuery += " [NameDiscipline], [Journal].[IdStudent], [Journal].[IdTeacher], [Journal].[IdDiscipline], [Journal].[IdEvaluation]";
                sqlQuery += " FROM [dbo].[Journal]";
                sqlQuery += " inner join [dbo].Student on Student.IdStudent = Journal.IdStudent";
                sqlQuery += " inner join [dbo].Teacher on Teacher.IdTeacher = Journal.IdTeacher";
                sqlQuery += " inner join [dbo].Evaluation on Evaluation.IdEvaluation = Journal.IdEvaluation";
                sqlQuery += " inner join [dbo].Discipline on Discipline.IdDiscipline = Journal.IdDiscipline";
                sqlQuery += " WHERE Student.IdUser = @IdUser or Teacher.IdUser=@IdUser";

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("IdUser", App.IdUser);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataTable);

                List<JournalModel> journalList = new List<JournalModel>();
                journalList = (from DataRow dataRow in dataTable.Rows
                    select new JournalModel()
                    {
                        IdJournal = Convert.ToInt32(dataRow["IdJournal"]),
                        FIOStudent = dataRow["FIOStudent"].ToString(),
                        FIOTeacher = dataRow["FIOTeacher"].ToString(),
                        NameDiscipline = dataRow["NameDiscipline"].ToString(),
                        NameEvaluation = dataRow["NameEvaluation"].ToString(),
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
            JournalModel tempJournal = new JournalModel();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdJournal, IdStudent, IdTeacher, IdDiscipline, IdEvaluation";
                    sqlQuery += " FROM [dbo].[Journal]";
                    sqlQuery += " WHERE [Journal].IdJournal = @IdJournal";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("IdJournal", idJournal);

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    List<JournalModel> items = new List<JournalModel>();
                    while (reader.Read())
                    {
                        JournalModel u = new JournalModel();
                        u.IdJournal = (int)reader["IdEvaluation"];
                        u.IdStudent = (int)reader["IdStudent"];
                        u.IdTeacher = (int)reader["IdTeacher"];
                        u.IdDiscipline = (int)reader["IdDiscipline"];
                        u.IdEvaluation= (int)reader["IdEvaluation"];

                        items.Add(u);
                    }
                    if (items.Count>0) tempJournal = items[0];

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
            return tempJournal;
        }
        public bool InsertJournal(JournalModel journalModel)
        {
            bool isInserted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "INSERT INTO dbo.[Journal] (IdStudent, IdTeacher, IdDiscipline, IdEvaluation) VALUES (@IdStudent, @IdTeacher, @IdDiscipline, @IdEvaluation)";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdStudent", journalModel.IdStudent);
                    sqlCommand.Parameters.AddWithValue("IdTeacher", journalModel.IdTeacher);
                    sqlCommand.Parameters.AddWithValue("IdDiscipline", journalModel.IdDiscipline);
                    sqlCommand.Parameters.AddWithValue("IdEvaluation", journalModel.IdEvaluation);
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


        public bool UpdateJournal(JournalModel journalModel)
        {
            bool isUpdated = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "UPDATE dbo.[Journal] set IdTeacher=@IdTeacher,IdStudent=@IdStudent,IdDiscipline=@IdDiscipline,IdEvaluation=@IdEvaluation" +
                                      " WHERE IdJournal=@IdJournal";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdJournal", journalModel.IdJournal);
                    sqlCommand.Parameters.AddWithValue("IdTeacher", journalModel.IdTeacher);
                    sqlCommand.Parameters.AddWithValue("IdStudent", journalModel.IdStudent);
                    sqlCommand.Parameters.AddWithValue("IdDiscipline", journalModel.IdDiscipline);
                    sqlCommand.Parameters.AddWithValue("IdEvaluation", journalModel.IdEvaluation);
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

        public bool DeleteJournal(int idJournal)
        {
            bool isDeleted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {

                try
                {
                    string sqlQuery = "DELETE FROM  dbo.[Journal] WHERE idJournal=@idJournal";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("idJournal", idJournal);
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



        #endregion  


        #region DisciplineAccess
        public List<DisciplineModel> GetDisciplineList()
        {
            List<DisciplineModel> tempDisciplineList = new List<DisciplineModel>();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdDiscipline, NameDiscipline";
                    sqlQuery += " FROM [dbo].[Discipline]";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<DisciplineModel> disciplines = new List<DisciplineModel>();
                        while (reader.Read())
                        {
                            DisciplineModel u = new DisciplineModel();
                            u.IdDiscipline = reader.GetInt32(0);
                            u.NameDiscipline = reader.GetString(1);
                            disciplines.Add(u);
                        }
                        tempDisciplineList = disciplines;
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
            return tempDisciplineList;
        }

        public DisciplineModel GetDiscipline(int idDiscipline)
        {
            DisciplineModel tempDiscipline = new DisciplineModel();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdDiscipline, NameDiscipline";
                    sqlQuery += " FROM [dbo].[Discipline]";
                    sqlQuery += " WHERE [Discipline].IdDiscipline = @IdDiscipline";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("IdDiscipline", idDiscipline);
                    
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    List<DisciplineModel> items = new List<DisciplineModel>();
                    while (reader.Read())
                    {
                        DisciplineModel u = new DisciplineModel();
                        u.IdDiscipline = (int)reader["IdDiscipline"];
                        u.NameDiscipline = (string)reader["NameDiscipline"];
                        items.Add(u);
                    }
                    if (items.Count > 0) tempDiscipline = items[0];

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
            return tempDiscipline;
        }
        public bool InsertDiscipline(DisciplineModel disciplineModel)
        {
            bool isInserted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "INSERT INTO dbo.[Discipline] (IdDiscipline,NameDiscipline) VALUES (@IdDiscipline, @NameDiscipline)";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdDiscipline", disciplineModel.IdDiscipline);
                    sqlCommand.Parameters.AddWithValue("NameDiscipline", disciplineModel.NameDiscipline);
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


        public bool UpdateDiscipline(DisciplineModel disciplineModel)
        {
            bool isUpdated = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "UPDATE dbo.[Discipline] set NameDiscipline=@NameDiscipline WHERE IdDiscipline=@IdDiscipline";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("NameDiscipline", disciplineModel.NameDiscipline);
                    sqlCommand.Parameters.AddWithValue("IdDiscipline", disciplineModel.IdDiscipline);
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

        public bool DeleteDiscipline(int idDiscipline)
        {
            bool isDeleted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {

                try
                {
                    string sqlQuery = "DELETE FROM  dbo.[Discipline] WHERE IdDiscipline=@IdDiscipline";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdDiscipline", idDiscipline);
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

        #endregion  


        #region EvaluationAccess
        public List<EvaluationModel> GetEvaluationList()
        {
            List<EvaluationModel> tempEvaluationList = new List<EvaluationModel>();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdEvaluation, NameEvaluation, NumberEvaluation";
                    sqlQuery += " FROM [dbo].[Evaluation]";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<EvaluationModel> items = new List<EvaluationModel>();
                        while (reader.Read())
                        {
                            EvaluationModel u = new EvaluationModel();
                            u.IdEvaluation = reader.GetInt32(0);
                            u.NameEvaluation = reader.GetString(1);
                            u.NumberEvaluation = reader.GetInt32(2);
                            items.Add(u);
                        }
                        tempEvaluationList = items;
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
            return tempEvaluationList;
        }

        public EvaluationModel GetEvaluation(int idEvaluation)
        {
            EvaluationModel tempEvaluation = new EvaluationModel();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdEvaluation, NameEvaluation, NumberEvaluation";
                    sqlQuery += " FROM [dbo].[Evaluation]";
                    sqlQuery += " WHERE [Evaluation].IdEvaluation = @IdEvaluation";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("IdEvaluation", idEvaluation);

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    List<EvaluationModel> items = new List<EvaluationModel>();
                    while (reader.Read())
                    {
                        EvaluationModel u = new EvaluationModel();
                        u.IdEvaluation = (int)reader["IdEvaluation"];
                        u.NameEvaluation = (string)reader["NameEvaluation"];
                        u.NumberEvaluation = (int)reader["NumberEvaluation"];

                        items.Add(u);
                    }
                    if (items.Count > 0) tempEvaluation = items[0];

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
            return tempEvaluation;
        }
        public bool InsertEvaluation(EvaluationModel evaluationModel)
        {
            bool isInserted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "INSERT INTO dbo.[Evaluation] (IdEvaluation, NameEvaluation, NumberEvaluation) VALUES (@IdEvaluation, @NameEvaluation,@NumberEvaluation)";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdDiscipline", evaluationModel.IdEvaluation);
                    sqlCommand.Parameters.AddWithValue("NameEvaluation", evaluationModel.NameEvaluation);
                    sqlCommand.Parameters.AddWithValue("NumberEvaluation", evaluationModel.NumberEvaluation);
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


        public bool UpdateEvaluation(EvaluationModel evaluationModel)
        {
            bool isUpdated = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "UPDATE dbo.[Evaluation] set NameEvaluation=@NameEvaluation,NumberEvaluation=@NumberEvaluation" +
                                      " WHERE IdEvaluation=@IdEvaluation";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("NameEvaluation", evaluationModel.NameEvaluation);
                    sqlCommand.Parameters.AddWithValue("NumberEvaluation", evaluationModel.NumberEvaluation);
                    sqlCommand.Parameters.AddWithValue("IdEvaluation", evaluationModel.IdEvaluation);
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

        public bool DeleteEvaluation(int IdEvaluation)
        {
            bool isDeleted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {

                try
                {
                    string sqlQuery = "DELETE FROM  dbo.[Evaluation] WHERE IdEvaluation=@IdEvaluation";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdEvaluation", IdEvaluation);
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

        #endregion


        #region TeacherAccess
        public List<TeacherModel> GetTeacherList()
        {
            List<TeacherModel> tempTeacherList = new List<TeacherModel>();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdUser, IdTeacher, LastNameTeacher, FirstNameTeacher,MiddleNameTeacher,DateOfBirthTeacher,NumberPhoneTeacher";
                    sqlQuery += " FROM [dbo].[Teacher]";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<TeacherModel> items = new List<TeacherModel>();
                        while (reader.Read())
                        {
                            TeacherModel u = new TeacherModel();
                            u.IdTeacher = reader.GetInt32(0);
                            u.LastNameTeacher = reader.GetString(1);
                            u.FirstNameTeacher = reader.GetString(2);
                            u.MiddleNameTeacher = reader.GetString(3);
                            u.DateOfBirthTeacher = reader.GetDateTime(4);
                            u.NumberPhoneTeacher = reader.GetString(5);
                            u.IdUser = reader.GetInt32(6);
                            items.Add(u);
                        }
                        tempTeacherList = items;
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
            return tempTeacherList;
        }

        public TeacherModel GetTeacher(int idUser)
        {
            TeacherModel tempTeacherModel = new TeacherModel();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdUser, IdTeacher, LastNameTeacher, FirstNameTeacher, MiddleNameTeacher, DateOfBirthTeacher, NumberPhoneTeacher";
                    sqlQuery += " FROM [dbo].[Teacher]";
                    sqlQuery += " WHERE [Teacher].IdUser = @IdUser";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("IdUser", idUser);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    List<TeacherModel> items = new List<TeacherModel>();
                    while (reader.Read())
                    {
                        TeacherModel u = new TeacherModel();
                        u.IdTeacher= (int)reader["IdTeacher"];
                        u.IdUser = (int)reader["IdUser"];
                        u.LastNameTeacher = (string)reader["LastNameTeacher"];
                        u.FirstNameTeacher = (string)reader["FirstNameTeacher"];
                        u.MiddleNameTeacher = (string)reader["MiddleNameTeacher"];
                        u.DateOfBirthTeacher = (DateTime)reader["DateOfBirthTeacher"];
                        u.NumberPhoneTeacher = (string)reader["NumberPhoneTeacher"];
                        items.Add(u);
                    }
                    if (items.Count > 0) tempTeacherModel = items[0];

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
            return tempTeacherModel;
        }
        public bool InsertTeacher(TeacherModel teacherModel)
        {
            bool isInserted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "INSERT INTO dbo.[Teacher] (IdUser,LastNameTeacher, FirstNameTeacher, MiddleNameTeacher,DateOfBirthTeacher,NumberPhoneTeacher) VALUES (@IdUser, @LastNameTeacher, @FirstNameTeacher,@MiddleNameTeacher,@DateOfBirthTeacher,@NumberPhoneTeacher)";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdUser", teacherModel.IdUser);
                    sqlCommand.Parameters.AddWithValue("LastNameTeacher", teacherModel.LastNameTeacher);
                    sqlCommand.Parameters.AddWithValue("FirstNameTeacher", teacherModel.FirstNameTeacher);
                    sqlCommand.Parameters.AddWithValue("MiddleNameTeacher", teacherModel.MiddleNameTeacher);
                    sqlCommand.Parameters.AddWithValue("DateOfBirthTeacher", teacherModel.DateOfBirthTeacher);
                    sqlCommand.Parameters.AddWithValue("NumberPhoneTeacher", teacherModel.NumberPhoneTeacher);
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


        public bool UpdateTeacher(TeacherModel teacherModel)
        {
            bool isUpdated = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "UPDATE dbo.[Teacher] set IdUser=@IdUser, LastNameTeacher=@PasswordUser, FirstNameTeacher=@FirstNameTeacher, DateOfBirthTeacher=@DateOfBirthTeacher, NumberPhoneTeacher=@NumberPhoneTeacher WHERE IdTeacher=@IdTeacher";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdUser", teacherModel.IdUser);
                    sqlCommand.Parameters.AddWithValue("LastNameTeacher", teacherModel.LastNameTeacher);
                    sqlCommand.Parameters.AddWithValue("FirstNameTeacher", teacherModel.FirstNameTeacher);
                    sqlCommand.Parameters.AddWithValue("MiddleNameTeacher", teacherModel.MiddleNameTeacher);
                    sqlCommand.Parameters.AddWithValue("DateOfBirthTeacher", teacherModel.DateOfBirthTeacher);
                    sqlCommand.Parameters.AddWithValue("NumberPhoneTeacher", teacherModel.NumberPhoneTeacher);
                    sqlCommand.Parameters.AddWithValue("IdTeacher", teacherModel.IdTeacher);
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

        public bool DeleteTeacher(int idUser)
        {
            bool isDeleted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {

                try
                {
                    string sqlQuery = "DELETE FROM  dbo.[Teacher] WHERE IdUser=@IdUser";
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

        #endregion


        #region StudentAccess
        public List<StudentModel> GetStudentList()
        {
            List<StudentModel> tempStudentList = new List<StudentModel>();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdUser, IdStudent, LastNameStudent, FirstNameStudent,MiddleNameStudent,DateOfBirthStudent,NumberPhoneStudent";
                    sqlQuery += " FROM [dbo].[Student]";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<StudentModel> items = new List<StudentModel>();
                        while (reader.Read())
                        {
                            StudentModel u = new StudentModel();
                            u.IdStudent = reader.GetInt32(0);
                            u.LastNameStudent = reader.GetString(1);
                            u.FirstNameStudent = reader.GetString(2);
                            u.MiddleNameStudent = reader.GetString(3);
                            u.DateOfBirthStudent = reader.GetDateTime(4);
                            u.NumberPhoneStudent = reader.GetString(5);
                            u.IdUser = reader.GetInt32(6);
                            items.Add(u);
                        }
                        tempStudentList = items;
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
            return tempStudentList;
        }

        public StudentModel GetStudent(int idUser)
        {
            StudentModel tempStudentModel = new StudentModel();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "SELECT IdUser, IdStudent, LastNameStudent, FirstNameStudent, MiddleNameStudent, DateOfBirthStudent, NumberPhoneStudent";
                    sqlQuery += " FROM [dbo].[Student]";
                    sqlQuery += " WHERE [Student].IdUser = @IdUser";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("IdUser", idUser);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    List<StudentModel> items = new List<StudentModel>();
                    while (reader.Read())
                    {
                        StudentModel u = new StudentModel();
                        u.IdStudent = (int)reader["IdStudent"];
                        u.IdUser = (int)reader["IdUser"];
                        u.LastNameStudent = (string)reader["LastNameStudent"];
                        u.FirstNameStudent = (string)reader["FirstNameStudent"];
                        u.MiddleNameStudent = (string)reader["MiddleNameStudent"];
                        u.DateOfBirthStudent = (DateTime)reader["DateOfBirthStudent"];
                        u.NumberPhoneStudent = (string)reader["NumberPhoneStudent"];
                        items.Add(u);
                    }
                    if (items.Count>0) tempStudentModel = items[0];

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
            return tempStudentModel;
        }
        public bool InsertStudent(StudentModel studentModel)
        {
            bool isInserted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "INSERT INTO dbo.[Student] (IdUser,LastNameStudent, FirstNameStudent, MiddleNameStudent, DateOfBirthStudent, NumberPhoneStudent) VALUES (@IdUser, @LastNameStudent, @FirstNameStudent, @MiddleNameStudent, @DateOfBirthStudent, @NumberPhoneStudent)";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdUser", studentModel.IdUser);
                    sqlCommand.Parameters.AddWithValue("LastNameStudent", studentModel.LastNameStudent);
                    sqlCommand.Parameters.AddWithValue("FirstNameStudent", studentModel.FirstNameStudent);
                    sqlCommand.Parameters.AddWithValue("MiddleNameStudent", studentModel.MiddleNameStudent);
                    sqlCommand.Parameters.AddWithValue("DateOfBirthStudent", studentModel.DateOfBirthStudent);
                    sqlCommand.Parameters.AddWithValue("NumberPhoneStudent", studentModel.NumberPhoneStudent);
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


        public bool UpdateStudent(StudentModel studentModel)
        {
            bool isUpdated = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                try
                {
                    string sqlQuery = "UPDATE dbo.[Student] set IdUser=@IdUser, LastNameStudent=@LastNameStudent, FirstNameStudent=@FirstNameStudent,MiddleNameStudent=@MiddleNameStudent, DateOfBirthStudent=@DateOfBirthStudent, NumberPhoneStudent=@NumberPhoneStudent WHERE IdStudent=@IdStudent";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("IdUser", studentModel.IdUser);
                    sqlCommand.Parameters.AddWithValue("LastNameStudent", studentModel.LastNameStudent);
                    sqlCommand.Parameters.AddWithValue("FirstNameStudent", studentModel.FirstNameStudent);
                    sqlCommand.Parameters.AddWithValue("MiddleNameStudent", studentModel.MiddleNameStudent);
                    sqlCommand.Parameters.AddWithValue("DateOfBirthStudent", studentModel.DateOfBirthStudent);
                    sqlCommand.Parameters.AddWithValue("NumberPhoneStudent", studentModel.NumberPhoneStudent);
                    sqlCommand.Parameters.AddWithValue("IdStudent", studentModel.IdStudent);
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

        public bool DeleteStudent(int idUser)
        {
            bool isDeleted = false;
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {

                try
                {
                    string sqlQuery = "DELETE FROM  dbo.[Student] WHERE IdUser=@IdUser";
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

        #endregion

        }
}
