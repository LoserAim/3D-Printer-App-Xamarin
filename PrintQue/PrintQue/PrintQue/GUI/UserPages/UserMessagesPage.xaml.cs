using PrintQue.Models;
using PrintQue.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMessagesPage : ContentPage
    {
        //MessageViewModel messageViewModel;
        private bool _dataLoaded = false;
        private ObservableCollection<MessageViewModel> _messages;
        public UserMessagesPage()
        {
            InitializeComponent();
            //messageViewModel = new MessageViewModel();
            //BindingContext = messageViewModel;
        }

        private async void sendButton_Clicked(object sender, EventArgs e)
        {
            var user = await UserViewModel.SearchByID(App.LoggedInUserID);
            var message = new MessageViewModel()
            {
                SenderId = App.LoggedInUserID,
                Sender = user,
            };

            var requests = user.Requests;
            var request = new RequestViewModel();
            if (requests.Count > 0)
            {
                request = requests.OrderByDescending(r => r.DateMade).FirstOrDefault();
                message.Request = request;
                message.RequestId = request.ID;
                message.TimeSent = DateTime.Now;
                message.Body = messageEntry.Text;
            }
            else
            {
                message.TimeSent = DateTime.Now;
                message.Body = messageEntry.Text;
            }
            await MessageViewModel.Insert(message);
            _messages.Add(message);
            MessageViewModel.PostMessage(message);
            //message.SendEmailWithGmail(Fill with recipient's email);
        }
        protected override void OnAppearing()
        {
            if (_dataLoaded)
                return;
            _dataLoaded = true;
            RefreshMessageListView();
            base.OnAppearing();
        }
        private async void RefreshMessageListView()
        {
            var mes = await MessageViewModel.SearchByUserID(App.LoggedInUserID);
            _messages = new ObservableCollection<MessageViewModel>(mes);
            Message_ListView.ItemsSource = _messages;
        }
        private void Message_ListView_Refreshing(object sender, EventArgs e)
        {
            RefreshMessageListView();
            Message_ListView.IsRefreshing = false;
            Message_ListView.EndRefresh();
        }
    }
}