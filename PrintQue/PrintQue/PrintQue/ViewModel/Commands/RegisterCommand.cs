using PrintQue.Models;
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
            var user = (UserViewModel)parameter;
            if (user == null)
                return false;
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.confirmPassword) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
                return false;
            if (!string.Equals(user.Password, user.confirmPassword))
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            RegisterViewModel.Register();
        }
    }
}
