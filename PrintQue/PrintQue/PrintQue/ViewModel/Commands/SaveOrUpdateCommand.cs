using System;
using System.Collections.Generic;
using System.Linq;
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
                if (string.IsNullOrEmpty(request.ProjectName) || string.IsNullOrEmpty(request.ProjectDescript))
                    return false;
                if(!request.ProjectDescript.Any(char.IsUpper) || !request.ProjectDescript.Any(char.IsLower))
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
                    viewModel.IsvisiblePrintTimeError = true;
                    return false;
                }
                if (string.IsNullOrEmpty(request.ProjectFilePath))
                {
                    viewModel.IsvisibleFileError = true;
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
