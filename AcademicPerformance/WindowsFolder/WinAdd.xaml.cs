using System;
using System.Windows;
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinAdd.xaml
    /// </summary>
    public partial class WinAdd
    {

        public Delegate UpdateActor;

        public WinAdd()
        {
            InitializeComponent();
            var addJournal = new VMAddJournal();
            this.DataContext = addJournal;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.RoleUser != 5) return;
            cbTeacher.IsEditable = false;
            cbTeacher.IsReadOnly = true;
            cbTeacher.IsHitTestVisible = false;
            cbTeacher.Focusable = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            UpdateActor.DynamicInvoke(); 
            this.Close();
        }
    }
}
