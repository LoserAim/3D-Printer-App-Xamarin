using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrintQue.ViewModel.Commands
{
    public class UserRequestCommand : ICommand
    {
        public UserMainRequestPrinterViewModel UserMainRequestPrinterViewModel { get; set; }
        public UserRequestCommand(UserMainRequestPrinterViewModel userMainRequestPrinterViewModel)
        {
            UserMainRequestPrinterViewModel = userMainRequestPrinterViewModel;
        }

        public event EventHandler CanExecuteChanged;
        
        public bool CanExecute(object parameter)
        {
            return true;

        }

        public void Execute(object parameter)
        {
            var printer = (Printer)parameter;
            UserMainRequestPrinterViewModel.CreateRequestDetails(printer);
        }
    }
}
