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
            viewModel.IsvisibleFileError = false;
            viewModel.IsvisiblePrintTimeError = false;
            viewModel.IsvisibleProjectDescError = false;
            if (request == null)
                return false;
            try
            {
                if (string.IsNullOrEmpty(request.ProjectName))
                    return false;
                if(string.IsNullOrEmpty(request.ProjectDescript))
                {
                    viewModel.IsvisibleProjectDescError = true;
                    return false;
                }
                if (string.IsNullOrEmpty(request.User.Email))
                    return false;
                if(string.IsNullOrEmpty(request.Printer.Name))
                    return false;
                if ((request.DateRequested.CompareTo(DateTime.Now) < 1))
                {
                    viewModel.IsvisiblePrintTimeError = false;
                    return false;
                }
                if (string.IsNullOrEmpty(request.ProjectFilePath))
                {
                    viewModel.IsvisibleFileError = false;
                    return false;
                }

            }
            catch (NullReferenceException)
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
