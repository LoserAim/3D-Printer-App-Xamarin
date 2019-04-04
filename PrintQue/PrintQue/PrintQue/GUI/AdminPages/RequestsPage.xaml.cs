
using PrintQue.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using PrintQue.GUI.DetailPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestsPage : ContentPage
	{

        public RequestsPage ()
		{
			InitializeComponent ();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            RequestListView.ItemsSource = await Request.SortByStatus("nostatus");
        }



        public void Clicked_Approve(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var request = menuItem.CommandParameter as Request;
            DisplayAlert("Approved", request.ProjectName, "OK");
        }

        public void Clicked_Deny(object sender, EventArgs e)
        {
            var request = (sender as MenuItem).CommandParameter as Request;
            DisplayAlert("Denied", request.ProjectName, "OK");
        }

        private async void RequestListView_Refreshing(object sender, EventArgs e)
        {
            RequestListView.ItemsSource = await Request.SortByStatus("nostatus");
            RequestListView.IsRefreshing = false;
            RequestListView.EndRefresh();
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var requests = await Request.SortByStatus("nostatus");
            var users = await User.GetAll();
            
            RequestListView.ItemsSource = requests.Where(r => r.ProjectName.Contains(e.NewTextValue) 
                || (users.SingleOrDefault(u => u.ID == r.UserID).Name.Contains(e.NewTextValue)));

        }

        async private void RequestListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var request = e.SelectedItem as Request;
            await Navigation.PushAsync(new RequestDetailPage(request));
            RequestListView.SelectedItem = null;
        }
    }
}