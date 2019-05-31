using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintQue.GUI.DetailPages;
using PrintQue.Helper;
using PrintQue.Models;
using PrintQue.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue
{


[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserMainPage : ContentPage
	{


		public UserMainPage ()
		{
			InitializeComponent ();

        }

        private ObservableCollection<PrinterViewModel> _printers;
        private bool isDataLoaded;
        public async void RefreshPrinterListView()
        {
            await AzureAppServiceHelper.SyncAsync();
            PrinterListView.IsRefreshing = true;
            var pri = await PrinterViewModel.GetAll();
            _printers = new ObservableCollection<PrinterViewModel>(pri);
            PrinterListView.ItemsSource = _printers;
            PrinterListView.IsRefreshing = false;
        }
        public async void SyncOfflineDatabase()
        {
            await AzureAppServiceHelper.SyncAsync();
        }
        protected override void OnAppearing()
        {
            if (isDataLoaded)
                return;
            isDataLoaded = true;
            RefreshPrinterListView();
            base.OnAppearing();

        }

        async private void PrinterListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var prichild = e.SelectedItem as PrinterViewModel;
            prichild = await PrinterViewModel.PopulateForeignKeys(prichild);
            string action = await DisplayActionSheet("What would you like to know about the Printer?"
                , "Projects Queued"
                , "Printer Color");
            switch(action)
            {
                case "Projects Queued":
                    await DisplayAlert("Projects Queued"
                        , "Here the number of projects queued for this printer today: " + prichild.Requests.Count, "OK");
                    break;
                case "Printer Color":
                    await DisplayAlert("Printer Color"
                        , "This printer only prints in " + prichild.PrintColor.Name, "OK");
                    break;
            }
            PrinterListView.SelectedItem = null;
        }

        private void  PrinterListView_Refreshing(object sender, EventArgs e)
        {
            RefreshPrinterListView();
            
            PrinterListView.EndRefresh();
        }
        private async void CreateRequestButton_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as Button;
            var selectedItem = menuItem.CommandParameter as PrinterViewModel;
            var request = new RequestViewModel()
            {
                Printer = selectedItem,
                PrinterId = selectedItem.ID,
                User = App.LoggedInUser,
                ApplicationUserId = App.LoggedInUser.ID,
            };
            await Navigation.PushAsync(new RequestDetailPage(request, 1));
        }
        
    }
}