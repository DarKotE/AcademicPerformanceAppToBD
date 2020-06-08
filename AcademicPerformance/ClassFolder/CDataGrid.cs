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

namespace AcademicPerformance.ClassFolder
{
    class CDataGrid
    {
        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
        SqlConnection sqlConnection = new SqlConnection(CSqlHelper.CnnVal("AcademicPerformanceDB"));
        SqlDataAdapter adapter;
        DataGrid dataGrid;
        DataTable dataTable;
        DataSet dataSet;

        public CDataGrid(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
        }

        public void LoadDG(string sqlQuery)
        {
            try
            {
                //SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-N9GUSG16;Initial Catalog=AcademicPerformance;Integrated Security=True");
                SqlConnection sqlConnection = new SqlConnection(CSqlHelper.CnnVal("AcademicPerformanceDB"));                
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                da.Fill(dt);


                dataGrid.ItemsSource = dt.DefaultView;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string SelectId()
        {
            object[] mass = null;
            string id = "";
            try
            {
                if (dataGrid != null) 
                {
                    DataRowView dataRowView = dataGrid.SelectedItem as DataRowView;
                    
                        if(dataRowView!=null)
                        {
                            DataRow dataRow = (DataRow)dataRowView.Row;
                            mass = dataRow.ItemArray;
                        }               
                }
                id = mass[0].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
            return id;
        }
    }
}
