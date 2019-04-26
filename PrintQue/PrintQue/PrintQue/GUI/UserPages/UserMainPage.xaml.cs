using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintQue.GUI.DetailPages;
using PrintQue.Models;
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

        private ObservableCollection<Printer> _printers;

        public async void GetAllChildren()
        {
            var pri = await Printer.GetAll();
            _printers = new ObservableCollection<Printer>(pri);
    
        }

        protected override void OnAppearing()
        {

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

        private void PrinterListView_Refreshing(object sender, EventArgs e)
        {
            GetAllChildren();
            PrinterListView.ItemsSource = _printers;
            PrinterListView.IsRefreshing = false;
            PrinterListView.EndRefresh();
        }
        private void CreateRequestButton_Clicked(object sender, EventArgs e)
        {
            Request request = null;
            Navigation.PushAsync(new RequestDetailPage(request));
        }
        async private void ToolbarItem_Run_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "You are about to logout. Are you sure?", "Yes", "No");
            if (response)
            {
                App.LoggedInUserID = -1;
                await Navigation.PopAsync();
            }
        }
    }
}