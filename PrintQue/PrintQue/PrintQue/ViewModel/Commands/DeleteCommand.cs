using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrintQue.ViewModel.Commands
{
    public class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public RequestDetailsViewModel viewModel;
        public DeleteCommand(RequestDetailsViewModel detailsViewModel)
        {
            viewModel = detailsViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.DeleteData();
        }
    }
}
