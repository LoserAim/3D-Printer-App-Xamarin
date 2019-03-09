using PrintQue.GUI.AdminPages;
using PrintQue.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestsPage : ContentPage
	{
        List<Request> GetRequests(string searchText = null)
        {
            List<Request> requests = new List<Request>();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                requests = conn.GetAllWithChildren<Request>().ToList();

            }
            var sortedRequests = requests.Where(g => g.status != null 
            || !g.status.Name.Contains("Approved") 
            || !g.status.Name.Contains ("Denied")).ToList();
            if (string.IsNullOrWhiteSpace(searchText))
                return sortedRequests;

            return sortedRequests.Where(g => g.ProjectName.StartsWith(searchText) || g.user.Email.StartsWith(searchText)).ToList();

        }
        public RequestsPage ()
		{
			InitializeComponent ();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RequestListView.ItemsSource = GetRequests();
        }



        public void Clicked_Approve(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var request = menuItem.CommandParameter as Request;
            DisplayAlert("Approve", request.ProjectName, "OK");
        }

        public void Clicked_Deny(object sender, EventArgs e)
        {
            var request = (sender as MenuItem).CommandParameter as Request;
            DisplayAlert("Deny", request.ProjectName, "OK");
        }

        private void RequestListView_Refreshing(object sender, EventArgs e)
        {
            RequestListView.ItemsSource = GetRequests();
            RequestListView.IsRefreshing = false;
            RequestListView.EndRefresh();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            RequestListView.ItemsSource = GetRequests(e.NewTextValue);
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