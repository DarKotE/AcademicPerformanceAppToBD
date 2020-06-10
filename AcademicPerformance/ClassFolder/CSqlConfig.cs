using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademicPerformance
{
    public static class CSqlConfig
    {
        public static string DefaultCnnVal()
        {
            return ConfigurationManager.ConnectionStrings["AcademicPerformanceDB"].ConnectionString;
        }
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        //public static SqlConnection SqlConn()
        //{
        //    return SqlConnection sqlConnection = new SqlConnection(CSqlConfig.DefaultCnnVal("AcademicPerformanceDB"));
        //}
    }
}
