using System.Diagnostics;
using System.Windows;
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    ///     Interaction logic for WinRoleEdit.xaml
    /// </summary>
    public partial class WinRoleEdit : Window
    {
        public WinRoleEdit()
        {
            InitializeComponent();
        }

        public delegate void Refresh();

        public event Refresh RefreshEvent;

        private void RefreshView()
        {
            var roleEdit = new VMRoleEdit();
            DataContext = null;
            DataContext = roleEdit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshView();
            tbSearch.Focus();

        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно желаете выйти?", "Информация",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                Process.Start(Application.ResourceAssembly.Location);
            }
        }


        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            var winAdd = new WinAdd();
            RefreshEvent += RefreshView;
            winAdd.UpdateActor = RefreshEvent;
            winAdd.Show();
        }


        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно желаете выйти?", "Информация",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                Process.Start(Application.ResourceAssembly.Location);
            }
        }
    }
}