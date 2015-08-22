using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pixiv_sekai
{
    class SimpleRelayCommand : ICommand
    {
        // Member variables
        private Action<object> ExecutionFunction;

        public SimpleRelayCommand(Action<object> executionFunction)
        {
            ExecutionFunction = executionFunction;
        }

        public bool CanExecute(object o)
        {
            return true;
        }

        public void Execute(object o)
        {
            ExecutionFunction(o);
        }

        public event EventHandler CanExecuteChanged;
    }
}
