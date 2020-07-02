using System;
using System.Windows.Input;

namespace AcademicPerformance.Classes
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> action)
        {
            DoAction = action;
        }

        public Action<object> DoAction { get; }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DoAction(parameter);
        }
    }
}