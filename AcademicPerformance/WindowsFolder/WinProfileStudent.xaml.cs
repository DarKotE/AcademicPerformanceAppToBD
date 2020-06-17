using System;
using System.Windows;
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Interaction logic for WinProfileStudent.xaml
    /// </summary>
    public partial class WinProfileStudent : Window
    {
        public Delegate UpdateActor;
        public WinProfileStudent()
        {
            InitializeComponent();
            var profileStudent = new VMProfileStudent();
            this.DataContext = profileStudent;
        }       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbStudLastName.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudLastName.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudName.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudName.Focus();
            }
            else if (string.IsNullOrEmpty(dpStudDateOfBirth.Text))
            {
                MessageBox.Show("Введите дату рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dpStudDateOfBirth.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudNumberPhone.Text))
            {
                MessageBox.Show("Введите номер телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudNumberPhone.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudLogin.Focus();
            }
            UpdateActor?.DynamicInvoke();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            UpdateActor?.DynamicInvoke();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
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
