using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrintQue.ViewModel;
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
                  ChatList.ScrollTo((this.BindingContext as ChatRoomViewModel).Messages.Last(), ScrollToPosition.End, false)
              );
            });
        }
        protected override void OnAppearing()
        {
            //if (_isDataLoaded)
            //    return;
            // _isDataLoaded = true;
            viewModel.UpdateChatList();
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
    }
}