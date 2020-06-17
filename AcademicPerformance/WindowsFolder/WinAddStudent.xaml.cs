using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using AcademicPerformance.ClassFolder;
using AcademicPerformance.ViewModelsFolder;

namespace AcademicPerformance.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для WinAddStudent.xaml
    /// </summary>
    public partial class WinAddStudent : Window
    {
        public WinAddStudent()
        {
            InitializeComponent();
            var profileStudent = new VMProfileStudent();
            this.DataContext = profileStudent;

        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbStudLastName.Text))
            {
                MessageBox.Show("Введите фамилию студента",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                tbStudLastName.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudName.Text))
            {
                MessageBox.Show("Введите имя студента",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                tbStudName.Focus();
            }
            else if (string.IsNullOrEmpty(dpStudDateOfBirth.Text))
            {
                MessageBox.Show("Введите дату рождения студента",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                dpStudDateOfBirth.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudNumberPhone.Text))
            {
                MessageBox.Show("Введите номер телефона студента",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                tbStudNumberPhone.Focus();
            }
            else if (string.IsNullOrEmpty(tbStudLogin.Text))
            {
                MessageBox.Show("Введите логин для студента",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                tbStudLogin.Focus();
            }
            
                    
                

            

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

