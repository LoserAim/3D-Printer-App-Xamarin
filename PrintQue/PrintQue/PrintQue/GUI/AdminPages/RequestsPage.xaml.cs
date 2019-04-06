
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
    public partial class RequestsPage : ContentPage
    {
        private ObservableCollection<RequestWithChildren> _requests;
        private bool isDataLoaded;
        public RequestsPage ()
		{
			InitializeComponent ();

        }

        protected override void OnAppearing()
        {

            if (isDataLoaded)
                return;
            isDataLoaded = true;
            RefreshRequestsView();

            base.OnAppearing();
        }

        public async void RefreshRequestsView()
        {
            var req = await Request.GetAll();
            var requ = new List<RequestWithChildren>();
            foreach (Request p in req)
            {
                var child = new RequestWithChildren()
                {
                    request = p,
                    user = await User.SearchByID(p.UserID),
                    printer = await Printer.SearchByID(p.PrinterID),
                    status = await Status.SearchByID(p.StatusID),
                };
                requ.Add(child);
            }
            _requests = new ObservableCollection<RequestWithChildren>(requ);
            RequestListView.ItemsSource = _requests;
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
            RefreshRequestsView();
            RequestListView.ItemsSource = _requests;
            RequestListView.IsRefreshing = false;
            RequestListView.EndRefresh();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshRequestsView();
            
            RequestListView.ItemsSource = _requests.Where(r => r.request.ProjectName.Contains(e.NewTextValue) 
                || r.user.Name.Contains(e.NewTextValue));

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