using PrintQue.GUI.DetailPages;
using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.ViewModel
{
    public class UserMainRequestPrinterViewModel
    {
        UserRequestCommand UserRequestCommand { get; set; }
        public UserMainRequestPrinterViewModel()
        {
            UserRequestCommand = new UserRequestCommand(this);
        }
        public async void CreateRequestDetails(Printer printer)
        {
            var request = new Request()
            {
                Printer = printer,
                PrinterID = printer.ID,
                UserID = App.LoggedInUserID,
                User = await User.SearchByID(App.LoggedInUserID),
            };
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RequestDetailPage(request, 1));

        }
    }
}
