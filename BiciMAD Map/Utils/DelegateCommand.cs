using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BiciMAD_Map.Utils
{
    class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<bool> canExecute;

        public DelegateCommand(Action<object> exec, Func<bool> canExec)
        {
            this.execute = exec;
            this.canExecute = canExec;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;

            return canExecute();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (execute != null)
                execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(null, new EventArgs());
        }
    }
}
