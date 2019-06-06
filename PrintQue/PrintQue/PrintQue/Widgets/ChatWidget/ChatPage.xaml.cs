using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrintQue.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.Widgets.ChatWidget
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : ContentPage
	{
        ChatRoomViewModel viewModel;
        public ICommand ScrollListCommand { get; set; }
        public ChatPage()
        {
            InitializeComponent();
            viewModel = new ChatRoomViewModel();
            this.BindingContext = viewModel;
            ScrollListCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        ChatList.ScrollTo((this.BindingContext as ChatRoomViewModel).Messages.Last(), ScrollToPosition.End, false);
                    }
                    catch (Exception)
            {

            }

        }
                    

              );
            });
        }
        protected override async void OnAppearing()
        {
            //if (_isDataLoaded)
            //    return;
            // _isDataLoaded = true;
            base.OnAppearing();
            viewModel.UpdateChatList();
            if ((await RequestViewModel.SearchByUser(App.LoggedInUser.ID)).Count < 1)
                await DisplayAlert("ALERT", "You have no requests. You must submit a request before you can message the admin.", "OK");
            base.OnAppearing();

        }

        public void ScrollTap(object sender, System.EventArgs e)
        {
            lock (new object())
            {
                if (BindingContext != null)
                {
                    var vm = BindingContext as ChatRoomViewModel;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        while (vm.DelayedMessages.Count > 0)
                        {
                            vm.Messages.Insert(0, vm.DelayedMessages.Dequeue());
                        }
                        vm.ShowScrollTap = false;
                        vm.LastMessageVisible = true;
                        vm.PendingMessageCount = 0;
                        ChatList?.ScrollToFirst();
                    });


                }

            }
        }

        public void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            chatInput.UnFocusEntry();
        }

        private void ChatList_Refreshing(object sender, EventArgs e)
        {
            
            viewModel.UpdateChatList();
        }
    }
}