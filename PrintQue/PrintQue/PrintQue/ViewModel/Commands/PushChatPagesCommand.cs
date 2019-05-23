using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrintQue.ViewModel.Commands
{
    public class PushChatPagesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public RequestDetailsViewModel viewModel { get; set; }
        public PushChatPagesCommand(RequestDetailsViewModel ViewModel)
        {
            viewModel = ViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return false;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
