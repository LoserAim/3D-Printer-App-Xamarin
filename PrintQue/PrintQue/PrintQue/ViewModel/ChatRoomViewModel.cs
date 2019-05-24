using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrintQue.ViewModel
{
    public class ChatRoomViewModel : INotifyPropertyChanged
    {
        private string _textToSend;
        private RequestViewModel _request;
        public ObservableCollection<MessageViewModel> Messages { get; set; } = new ObservableCollection<MessageViewModel>();
        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                OnPropertyChanged("TextToSend");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public bool ShowScrollTap { get; set; } = false; //Show the jump icon 
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }
        public Queue<MessageViewModel> DelayedMessages { get; set; } = new Queue<MessageViewModel>();
        public ICommand MessageAppearingCommand { get; set; }
        public ICommand MessageDisappearingCommand { get; set; }
        public ICommand OnSendCommand { get; set; }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ChatRoomViewModel(RequestViewModel request=null)
        {
            Messages = new ObservableCollection<MessageViewModel>();
            //When you send a message where the SenderID is equal to the App.LoggedInUser.ID then the message will be blue
            //Messages.Insert(0, new MessageViewModel() { Body = "Hi" });
            //Messages.Insert(0, new MessageViewModel() { Body = "How are you?", Sender = App.LoggedInUser, SenderId = App.LoggedInUser.ID });
            MessageAppearingCommand = new Command<MessageViewModel>(OnMessageAppearing);
            MessageDisappearingCommand = new Command<MessageViewModel>(OnMessageDisappearing);
            OnSendCommand = new Command(() =>
            {

                SendMessage();
            });
            if (request != null)
            {
                SetMessages(request);
                _request = request;
            }



        }
        public async void UpdateChatList()
        {
            
            var user = await UserViewModel.SearchByID(App.LoggedInUser.ID);
            if (user.Requests != null)
            {
                Messages.Clear();
                var messi = new List<MessageViewModel>();
                foreach (var n in user.Requests)
                {
                    foreach (var m in (await MessageViewModel.SearchByRequestID(n.ID)))
                    {
                        Messages.Insert(0, new MessageViewModel() { Body = m.Body, SenderId = m.SenderId });
                    }
                }
                Messages.OrderBy(m => m.TimeSent);
            }
        }
        public async void SetMessages(RequestViewModel request)
        {
            Messages = new ObservableCollection<MessageViewModel>(await MessageViewModel.SearchByUserID(request.ApplicationUserId));
        }
        public async void SendMessage()
        {
            var re = (await RequestViewModel.SearchByUser(App.LoggedInUser.ID)).Count();
            if (!string.IsNullOrEmpty(TextToSend) && re > 0)
            {
                var messi = new MessageViewModel()
                {
                    Body = TextToSend,
                    SenderId = App.LoggedInUser.ID,

                };
                Messages.Insert(0, messi);
                TextToSend = string.Empty;
                messi.Sender = App.LoggedInUser;
                messi.Request = (await RequestViewModel.SearchByUser(App.LoggedInUser.ID)).OrderByDescending(r => r.DateMade).FirstOrDefault();
                messi.RequestId = messi.Request.ID;
                messi.TimeSent = DateTime.Now;
                var user = await UserViewModel.SearchByID(App.LoggedInUser.ID);
                user.LatestMessage = DateTime.Now;
                await UserViewModel.UpdateUser(user);
                await MessageViewModel.Insert(messi);
                
            }
            else if (re == 0)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Alert", "You have not made a request! Go create one to message an admin.", "OK");

            }
        }
        void OnMessageAppearing(MessageViewModel message)
        {
            var idx = Messages.IndexOf(message);
            if (idx <= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    while (DelayedMessages.Count > 0)
                    {
                        Messages.Insert(0, DelayedMessages.Dequeue());
                    }
                    ShowScrollTap = false;
                    LastMessageVisible = true;
                    PendingMessageCount = 0;
                });
            }
        }
        void OnMessageDisappearing(MessageViewModel message)
        {
            var idx = Messages.IndexOf(message);
            if (idx >= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });

            }
        }
    }
}
