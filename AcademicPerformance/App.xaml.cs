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
        public static string IdUser { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static string Role { get; set; }       
    }
}
