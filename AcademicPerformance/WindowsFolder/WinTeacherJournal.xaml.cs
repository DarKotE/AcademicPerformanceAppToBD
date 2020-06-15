using System.Windows;
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinTeacher.xaml
    /// </summary>
    public partial class WinTeacher : Window
    {

        public delegate void Refresh();
        public event Refresh RefreshEvent;

        private void RefreshView()
        {
            var teacherJournal = new VMTeacherJournal();
            this.DataContext = null;
            this.DataContext = teacherJournal;
        }

        public WinTeacher()
        {
            InitializeComponent();
            RefreshView();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbSearch.Focus();
        }

        
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно желаете выйти?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }
        }
        

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            WinAdd winAdd = new WinAdd();
            RefreshEvent += new Refresh(RefreshView);
            winAdd.UpdateActor = RefreshEvent;
            winAdd.Show();
        }

        private void miProfile_Click(object sender, RoutedEventArgs e)
        {
            RefreshEvent += new Refresh(RefreshView);
            WinProfileTeacher winProfileTeacher = new WinProfileTeacher();
            winProfileTeacher.UpdateActor = RefreshEvent;
            winProfileTeacher.Show();
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно желаете выйти?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }
        }
    }
}


