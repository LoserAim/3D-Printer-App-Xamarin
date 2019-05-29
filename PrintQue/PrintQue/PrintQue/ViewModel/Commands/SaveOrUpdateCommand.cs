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
            try
            {
                if (string.IsNullOrEmpty(request.ProjectName) || string.IsNullOrEmpty(request.ProjectDescript)
                || string.IsNullOrEmpty(request.User.Email)
                || string.IsNullOrEmpty(request.Printer.Name))
                    return false;
                if ((request.DateRequested.CompareTo(DateTime.Now) < 1)
                    || string.IsNullOrEmpty(request.ProjectFilePath))
                    return false;
            }
            catch (NullReferenceException nre)
            {
                return false;
            }



            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.SaveData();
        }
    }
}
