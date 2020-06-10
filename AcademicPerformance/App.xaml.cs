using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AcademicPerformance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static int IdUser { get; set; }
        public static string LoginUser { get; set; }
        public static string PasswordUser { get; set; }
        public static int RoleUser { get; set; }       
    }
}
