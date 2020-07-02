using System.Windows;

namespace AcademicPerformance
{
    /// <summary>
    /// Global values for current user session in application
    /// </summary>
    public partial class App
    {
        public static int IdUser { get; set; }
        public static string LoginUser { get; set; }
        public static string PasswordUser { get; set; }
        public static int RoleUser { get; set; }       
    }
}
