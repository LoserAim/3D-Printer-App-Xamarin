using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrintQue.ViewModel.Commands
{
    public class SaveOrUpdateCommand : ICommand
    {
        RequestDetailsViewModel viewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public SaveOrUpdateCommand(RequestDetailsViewModel ViewModel)
        {
            viewModel = ViewModel;
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
