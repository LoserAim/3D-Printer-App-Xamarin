using PrintQue.GUI.DetailPages;
using PrintQue.Models;
using PrintQue.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.UserPages
{
    //public class StatusToStatusNameConverter : IValueConverter
    //{

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var statusName = (string)value;

    //        SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation);
    //        List<Status>     statuses   = connection.Table<Status>().ToList();

    //        connection.CreateTable<Status>();

    //        return statuses.Where(s => s.Name == statusName).First().ID;
    //    }

    //    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var statusID  = (string)value;

    //        var item = stats;


    //        return item.Name;      
    //    }
    //}

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserSubmissionsPage : ContentPage
	{
        private ObservableCollection<RequestViewModel> _requests;
        private bool _isDataLoaded = false;
        public UserSubmissionsPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            if (_isDataLoaded)
                return;
            _isDataLoaded = true;
            RefreshRequestsView();
            base.OnAppearing();
        }

        public async void RefreshRequestsView()
        {
            var requests = await RequestViewModel.GetAll();
            var UserRequests = new List<RequestViewModel>();
            UserRequests = requests.Where(u => u.UserID == App.LoggedInUserID).ToList();
            _requests = new ObservableCollection<RequestViewModel>(UserRequests);
            RequestListView.ItemsSource = _requests;
        }

        private void RequestListView_Refreshing(object sender, System.EventArgs e)
        {
            RequestListView.BeginRefresh();            
            RefreshRequestsView();     
            RequestListView.EndRefresh();
        }

        async private void RequestListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var request = e.SelectedItem as RequestViewModel;
            await Navigation.PushAsync(new RequestDetailPage(request, 3));
            RequestListView.SelectedItem = null;
        }
    }
}