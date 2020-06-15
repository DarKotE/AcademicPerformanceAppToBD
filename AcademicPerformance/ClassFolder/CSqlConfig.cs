using System.Configuration;

namespace AcademicPerformance.ClassFolder
{
    public static class CSqlConfig
    {
        public static string DefaultCnnVal(string name = "AcademicPerformanceDB")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
