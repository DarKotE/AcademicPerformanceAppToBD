using System.Windows;
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinAdminJournal.xaml
    /// </summary>
    public partial class WinAdminJournal: Window
    {

        public delegate void Refresh();
        public event Refresh RefreshEvent;

        private void RefreshView()
        {
            var adminJournal = new VMJournal();
            this.DataContext = null;
            this.DataContext = adminJournal;
        }

        public WinAdminJournal()
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


