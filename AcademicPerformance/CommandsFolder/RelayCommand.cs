using System;
using System.Windows.Input;

namespace AcademicPerformance.CommandsFolder
{
    public class RelayCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object> DoAction { get; }


        public RelayCommand(Action<object> action)
        {
            DoAction = action;
        }

   

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
