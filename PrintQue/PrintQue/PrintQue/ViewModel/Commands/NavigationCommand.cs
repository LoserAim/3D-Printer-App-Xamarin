using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrintQue.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public UserMainViewModel _UserMainViewModel {get; set;}
        public NavigationCommand(UserMainViewModel userMainViewModel)
        {
            _UserMainViewModel = userMainViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
