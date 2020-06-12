using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcademicPerformance.CommandsFolder
{
    public class RelayCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> doAction;
        private Action<object> save;

        public RelayCommand(Action<object> action)
        {
            doAction = action;
        }

   

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            doAction(parameter);
        }

        

    }
}
