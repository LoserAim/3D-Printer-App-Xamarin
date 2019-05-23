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
            var request = (RequestViewModel)parameter;
            if (request == null)
                return false;
            if (string.IsNullOrEmpty(request.ProjectName) || string.IsNullOrEmpty(request.ProjectDescript))
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.SaveData();
        }
    }
}
