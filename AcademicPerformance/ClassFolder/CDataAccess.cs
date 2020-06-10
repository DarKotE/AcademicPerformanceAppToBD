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
 
        public bool isAuthValid(string login, string password)
        {
            
            using (SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal()))
            {
                SqlCommand sqlCommand;
                SqlDataReader sqlDataReader;
                sqlCommand = new SqlCommand("select PasswordUser, RoleUser, IdUser From dbo.[User] Where  LoginUser='" + login + "'", sqlConnection);
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                if (!sqlDataReader.Read() || (sqlDataReader[0].ToString() != password))
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
        public CUser GetUser(string userLogin, string userPassword)
        {
            CUser tempUser = new CUser();
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
                    List<CUser> users = new List<CUser>();
                    while (reader.Read())
                    {
                        CUser u = new CUser();
                        u.IdUser = (int)reader["IdUser"];
                        u.LoginUser = (string)reader["LoginUser"];
                        u.PasswordUser = (string)reader["PasswordUser"];
                        u.RoleUser = (int)reader["RoleUser"];
                        users.Add(u);
                    }
                    tempUser = users[0];

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

        public List<CJournalVal> GetJournalList(DataTable dataTable)
        {
            List<CJournalVal> studentList = new List<CJournalVal>();
            studentList = (from DataRow dataRow in dataTable.Rows
                           select new CJournalVal()
                           {
                               IdJournal = Convert.ToInt32(dataRow["IdJournal"]),
                               FIOStudent = dataRow["FIOStudent"].ToString(),
                               FIOTeacher = dataRow["FIOTeacher"].ToString(),
                               NameDiscipline = dataRow["NameDiscipline"].ToString(),
                               NameEvaluation = dataRow["NameEvaluation"].ToString(),
                               NumberEvaluation = dataRow["NumberEvaluation"].ToString(),
                           }).ToList();

            var output = studentList;
            return output;
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
