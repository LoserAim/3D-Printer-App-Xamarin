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

        private ObservableCollection<Printer> _printers;

        public async void GetAllChildren()
        {
            var pri = await Printer.GetAll();
            _printers = new ObservableCollection<Printer>(pri);
    
        }

        protected override async void OnAppearing()
        {
            await AzureAppServiceHelper.SyncAsync();
            GetAllChildren();
            PrinterListView.ItemsSource = _printers;
            base.OnAppearing();

        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetAllChildren();

            PrinterListView.ItemsSource = _printers.Where(p => p.Name.Contains(e.NewTextValue)).ToList();

        }
        async private void PrinterListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var prichild = e.SelectedItem as Printer;
            var request = new Request() { Printer = prichild, PrinterID = prichild.ID };
            await Navigation.PushAsync(new RequestDetailPage(request));
            PrinterListView.SelectedItem = null;
        }

        private async void  PrinterListView_Refreshing(object sender, EventArgs e)
        {
            await AzureAppServiceHelper.SyncAsync();

            GetAllChildren();
            PrinterListView.ItemsSource = _printers;
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