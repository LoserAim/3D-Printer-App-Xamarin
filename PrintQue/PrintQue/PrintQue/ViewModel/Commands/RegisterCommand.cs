using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrintQue.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public RegisterCommand(RegisterViewModel registerViewModel)
        {
            RegisterViewModel = registerViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
