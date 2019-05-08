using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrintQue.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginViewModel ViewModel { get; set; }

        public LoginCommand(LoginViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var user = (User)parameter;
            if (user == null)
                return false;
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return false;
            
            return true;
        }
        
        public void Execute(object parameter)
        {
            ViewModel.Login();
        }
    }
}
