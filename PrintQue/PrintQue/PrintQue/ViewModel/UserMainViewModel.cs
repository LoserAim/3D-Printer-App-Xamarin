using PrintQue.GUI.DetailPages;
using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrintQue.ViewModel
{
    public class UserMainViewModel : BaseViewModel
    {
        public ICommand CreateRequestDetailsCommand { get; private set; }
        public ICommand SelectPrinterCommand { get; private set; }
        public NavigationCommand NavCommand { get; set; }
        private Printer selectedPrinter;

        public ObservableCollection<Printer> Printers { get; private set; } = new ObservableCollection<Printer>(Printer.GetAll().Result);
       

        public Printer SelectedPrinter { get => selectedPrinter; set => SetValue(ref selectedPrinter, value); }



        public UserMainViewModel(IPageService pageService)
        {
            NavCommand = new NavigationCommand(this);
            _pageService = pageService;
            CreateRequestDetailsCommand = new Command(CreateRequest);
           /* SelectPrinterCommand = new Command<PrinterViewModel>(async vm => await SelectPrinter(vm));
           */
        }
        private readonly IPageService _pageService;
        private async void CreateRequest()
        {
            Request request = null;
            await _pageService.PushAsync(new RequestDetailPage(request, 1));
        }

        public async void SelectPrinter(Printer _printer)
        {
            if (_printer == null)
                return;
            // This method should display information important to the user
            var request = new Request() { Printer = _printer, PrinterID = _printer.ID };
            await _pageService.PushAsync(new RequestDetailPage(request, 1));
            SelectedPrinter = null;
            //Code for printer information
        }
        public void Navigate()
        {
            Request request = null;
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RequestDetailPage(request, 1));
        }
    }
}
