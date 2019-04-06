
using PrintQue.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using PrintQue.GUI.DetailPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace PrintQue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
<<<<<<< HEAD
    public partial class RequestsPage : ContentPage
    {
        public ObservableCollection<Request> _requests;
=======
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
>>>>>>> parent of e8b7215... Implementing async features
        public RequestsPage ()
		{
			InitializeComponent ();

        }

        protected override void OnAppearing()
        {
            RefreshRequestsView();
            RequestListView.ItemsSource = _requests;
            base.OnAppearing();
<<<<<<< HEAD
=======
            RequestListView.ItemsSource = GetRequests();
>>>>>>> parent of e8b7215... Implementing async features
        }

        public async void RefreshRequestsView()
        {
            var req = await Request.GetAll();

            _requests = new ObservableCollection<Request>(req);
        }

        public void Clicked_Approve(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var request = menuItem.CommandParameter as RequestWithChildren;
            DisplayAlert("Approved", request.request.ProjectName, "OK");
        }

        public void Clicked_Deny(object sender, EventArgs e)
        {
            var request = (sender as MenuItem).CommandParameter as RequestWithChildren;
            DisplayAlert("Denied", request.request.ProjectName, "OK");
        }

        private void RequestListView_Refreshing(object sender, EventArgs e)
        {
<<<<<<< HEAD
            RefreshRequestsView();
            RequestListView.ItemsSource = _requests;
=======
            RequestListView.ItemsSource = GetRequests();
>>>>>>> parent of e8b7215... Implementing async features
            RequestListView.IsRefreshing = false;
            RequestListView.EndRefresh();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
<<<<<<< HEAD
            RefreshRequestsView();
            
            RequestListView.ItemsSource = _requests.Where(r => r.ProjectName.Contains(e.NewTextValue) 
                || r.user.Name.Contains(e.NewTextValue));

=======
            RequestListView.ItemsSource = GetRequests(e.NewTextValue);
>>>>>>> parent of e8b7215... Implementing async features
        }

        async private void RequestListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var request = e.SelectedItem as RequestWithChildren;
            await Navigation.PushAsync(new RequestDetailPage(request.request));
            RequestListView.SelectedItem = null;
        }
    }
}