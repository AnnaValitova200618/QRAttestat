using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QRAttestat
{
    public class CustomCommand : ICommand
    {
        Action Action;
        public CustomCommand(Action action)
        {
            Action = action;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
           Action();
        }
    }
}
