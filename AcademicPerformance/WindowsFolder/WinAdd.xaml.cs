﻿using System;
using System.Windows;
using AcademicPerformance.ClassFolder;
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
            if (App.RoleUser != Const.RoleValue.Teacher) return;
            cbTeacher.IsEditable = false;
            cbTeacher.IsReadOnly = true;
            cbTeacher.IsHitTestVisible = false;
            cbTeacher.Focusable = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UpdateActor.DynamicInvoke();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            UpdateActor.DynamicInvoke(); 
            this.Close();
        }
    }
}
