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
            viewModel = new UserMainViewModel();
            BindingContext = viewModel;
        }



        public async void SyncOfflineDatabase()
        {
            await AzureAppServiceHelper.SyncAsync();
        }
        protected override void OnAppearing()
        {


            base.OnAppearing();

        }

        async private void PrinterListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var prichild = e.SelectedItem as Printer;
            var request = new Request() { Printer = prichild, PrinterID = prichild.ID };
            await Navigation.PushAsync(new RequestDetailPage(request, 1));
            PrinterListView.SelectedItem = null;
        }

        private void  PrinterListView_Refreshing(object sender, EventArgs e)
        {
            viewModel = new UserMainViewModel(new PageService());
            BindingContext = viewModel;
            PrinterListView.IsRefreshing = false;
            PrinterListView.EndRefresh();
        }

        private void PrinterListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            viewModel.SelectPrinterCommand.Execute(e.SelectedItem);
        }
        private void PrinterListView_Button(object sender, EventArgs e)
        {
            var menuItem = sender as Button;
            var selectedItem = menuItem.CommandParameter as Printer;
            viewModel.CreateRequestDetailsCommand.Execute(selectedItem);
        }
        //private void CreateRequestButton_Clicked(object sender, EventArgs e)
        //{
        //    Request request = null;


        //    Navigation.PushAsync(new RequestDetailPage(request));
        //}

    }
}