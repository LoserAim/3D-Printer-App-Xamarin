using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrintQue.ViewModel.Commands
{
    public class OnSendCommand : ICommand
    {
        public ChatRoomViewModel viewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public OnSendCommand(ChatRoomViewModel _viewModel)
        {
            viewModel = _viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.SendMessage();
        }
    }
}
