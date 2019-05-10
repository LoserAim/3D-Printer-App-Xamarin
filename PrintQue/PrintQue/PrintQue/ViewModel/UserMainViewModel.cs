using PrintQue.GUI.DetailPages;
using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrintQue.ViewModel
{
    public class UserMainViewModel : BaseViewModel
    {
        public ICommand CreateRequestDetailsCommand { get; private set; }
        public ICommand SelectPrinterCommand { get; private set; }
        private Printer selectedPrinter;

        public ObservableCollection<Printer> Printers { get;
            private set; } = new ObservableCollection<Printer>();
       

        public Printer SelectedPrinter { get => selectedPrinter;
            set => SetValue(ref selectedPrinter, value); }



        public UserMainViewModel(IPageService pageService)
        {
            var temp = Printer.GetAll().Result;
            Printers = new ObservableCollection<Printer>(temp);
            _pageService = pageService;
            CreateRequestDetailsCommand = new Command<Printer>(async vm => await CreateRequest(vm));
            SelectPrinterCommand = new Command<Printer>(async vm => await SelectPrinter(vm));
           
        }
        private readonly IPageService _pageService;
        private async Task CreateRequest(Printer printer)
        {
            Request request = new Request()
            {
                Printer = printer,
                PrinterID = printer.ID,
                UserID = App.LoggedInUserID,
                User = await User.SearchByID(App.LoggedInUserID),
            };

            await _pageService.PushAsync(new RequestDetailPage(request, 1));
        }

        private async Task SelectPrinter(Printer _printer)
        {
            if (_printer == null)
                return;
            string projects = "This is how many projects are queued: " + _printer.ProjectsQueued;
            // This method should display information important to the user
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Printer Details", projects, "Ok");

            //Code for printer information
        }

    }
}
