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
        UserMainViewModel viewModel;

		public UserMainPage ()
		{
			InitializeComponent ();
            viewModel = new UserMainViewModel(new PageService());
            BindingContext = viewModel;
        }

        private ObservableCollection<Printer> _printers;
        private bool isDataLoaded;
        public async void RefreshPrinterListView()
        {
            var pri = await Printer.GetAll();
            _printers = new ObservableCollection<Printer>(pri);
            PrinterListView.ItemsSource = _printers;
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



        private void  PrinterListView_Refreshing(object sender, EventArgs e)
        {
            RefreshPrinterListView();
            PrinterListView.IsRefreshing = false;
            PrinterListView.EndRefresh();
        }
        //private void CreateRequestButton_Clicked(object sender, EventArgs e)
        //{
        //    Request request = null;
            

        //    Navigation.PushAsync(new RequestDetailPage(request));
        //}
        
    }
}