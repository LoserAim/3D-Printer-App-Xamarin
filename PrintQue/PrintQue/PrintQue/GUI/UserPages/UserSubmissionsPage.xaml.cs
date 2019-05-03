using PrintQue.GUI.DetailPages;
using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.UserPages
{
    public class StatusToStatusNameConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var statusName = (string)value;

            SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation);
            List<Status>     statuses   = connection.Table<Status>().ToList();

            connection.CreateTable<Status>();

            return statuses.Where(s => s.Name == statusName).First().ID;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var statusID  = (int)value;
            
            SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation);
            List<Status>     statuses   = connection.Table<Status>().ToList();

            connection.CreateTable<Status>();

            return statuses.Where(s => s.ID == statusID).First().Name;      
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserSubmissionsPage : ContentPage
	{
        private ObservableCollection<Request> _requests;
        
        public UserSubmissionsPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            RefreshRequestsView();
            base.OnAppearing();
        }

        public async void RefreshRequestsView()
        {
            var requests = await Request.GetAll();
            var UserRequests = new List<Request>();
            UserRequests = requests.Where(u => u.UserID == App.LoggedInUserID).ToList();
            _requests = new ObservableCollection<Request>(UserRequests);
            RequestListView.ItemsSource = _requests;
        }

        private void RequestListView_Refreshing(object sender, System.EventArgs e)
        {
            RequestListView.BeginRefresh();            
            RefreshRequestsView();
            RequestListView.ItemsSource = _requests;         
            RequestListView.EndRefresh();
        }

        async private void RequestListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var request = e.SelectedItem as Request;
            await Navigation.PushAsync(new RequestDetailPage(request, 3));
            RequestListView.SelectedItem = null;
        }
    }
}