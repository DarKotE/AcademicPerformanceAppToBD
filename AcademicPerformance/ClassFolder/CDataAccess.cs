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


namespace AcademicPerformance
{
    public class CDataAccess
    {
        public DataTable GetJournalTableVar()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(CSqlHelper.CnnVal("AcademicPerformanceDB")))
            {
                try
                {
                    string sqlQuery = "select  RTRIM(LTRIM(CONCAT(COALESCE([LastNameStudent] + ' ', ''), COALESCE([FirstNameStudent] + ' ', ''), COALESCE([MiddleNameStudent], '')))) AS FIOStudent,";
                    sqlQuery += "[NameEvaluation],[NumberEvaluation],";
                    sqlQuery += "RTRIM(LTRIM(CONCAT(COALESCE([LastNameTeacher] + ' ', ''), COALESCE([FirstNameTeacher] + ' ', ''), COALESCE([MiddleNameTeacher], '')))) AS FIOTeacher,";
                    sqlQuery += "[NameDiscipline] ";
                    sqlQuery += "FROM [dbo].[Journal]";
                    sqlQuery += " inner join [dbo].Student on Student.IdStudent = Journal.IdStudent";
                    sqlQuery += " inner join [dbo].Teacher on Teacher.IdTeacher = Journal.IdTeacher";
                    sqlQuery += " inner join [dbo].Evaluation on Evaluation.IdEvaluation = Journal.IdEvaluation";
                    sqlQuery += " inner join [dbo].Discipline on Discipline.IdDiscipline = Journal.IdDiscipline";

                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    
                }
                

            }
            return dataTable;
        }
    }
}
