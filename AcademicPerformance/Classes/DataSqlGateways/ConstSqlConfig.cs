using System.Configuration;

namespace AcademicPerformance.Classes.DataSqlGateways
{
    public static class ConstSqlConfig
    {
        //connection string value in App.config
        public static string DefaultCnnVal(string name = "AcademicPerformanceDB")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
