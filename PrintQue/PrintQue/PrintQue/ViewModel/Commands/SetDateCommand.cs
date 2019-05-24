using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrintQue.ViewModel.Commands
{
    public class SetDateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public RequestDetailsViewModel viewModel { get; set; }
        public SetDateCommand(RequestDetailsViewModel ViewModel)
        {
            viewModel = ViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.OpenDatePicker();
        }
    }
}
