using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Linq;

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
            RegisterViewModel.IsvisibleEmailError = false;
            RegisterViewModel.IsvisiblePasswordError = false;
            RegisterViewModel.IsvisiblePasswordError2 = false;
            var user = (UserViewModel)parameter;
            if (user == null)
                return false;
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.confirmPassword) || string.IsNullOrEmpty(user.First_Name) || string.IsNullOrEmpty(user.Last_Name))
                return false;
            if (!user.Email.Contains("@"))
            {
                RegisterViewModel.IsvisibleEmailError = true;
                return false;
            }
            else
            {
                RegisterViewModel.IsvisibleEmailError = false;
            }
            if (!user.Password.Any(char.IsUpper) || !user.Password.Any(char.IsLower) || (!user.Password.Any(char.IsSymbol) && !user.Password.Any(char.IsDigit) && !user.Password.Any(char.IsPunctuation)))
            {
                RegisterViewModel.IsvisiblePasswordError = true;
                return false;
            }
            else
            {
                RegisterViewModel.IsvisiblePasswordError = false;
            }
            if (!string.Equals(user.Password, user.confirmPassword))
            {
                RegisterViewModel.IsvisiblePasswordError2 = true;
                return false;
            } else
            {
                RegisterViewModel.IsvisiblePasswordError2 = false;
            }
            
            return true;
        }

        public void Execute(object parameter)
        {
            RegisterViewModel.Register();
        }
    }
}
