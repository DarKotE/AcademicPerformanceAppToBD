using System.Windows;
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для WinAddTeacher.xaml
    /// </summary>
    public partial class WinAddTeacher : Window
    {
        public WinAddTeacher()
        {
            InitializeComponent();
            var profileTeacher = new VMProfileTeacher();
            this.DataContext = profileTeacher;
        }
        

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbStudLastName.Text))
            {
                MessageBox.Show("Введите фамилию преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudLastName.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudName.Text))
            {
                MessageBox.Show("Введите имя преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudName.Focus();
            }
            else if (string.IsNullOrEmpty(dpStudDateOfBirth.Text))
            {
                MessageBox.Show("Введите дату рождения преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dpStudDateOfBirth.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudNumberPhone.Text))
            {
                MessageBox.Show("Введите номер телефона преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudNumberPhone.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudLogin.Text))
            {
                MessageBox.Show("Введите логин для преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbStudLogin.Focus();
            }
            

           
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
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
