using System.Configuration;

namespace AcademicPerformance.ClassFolder
{
    public static class CSqlConfig
    {
        //connection string value in App.config
        public static string DefaultCnnVal(string name = "AcademicPerformanceDB")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
