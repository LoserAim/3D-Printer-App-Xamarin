using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrintQue.ViewModel
{
    public class ChatRoomViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<MessageViewModel> Messages { get; set; } = new ObservableCollection<MessageViewModel>();
        public string TextToSend { get; set; }
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
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    Messages.Insert(0, new MessageViewModel() { Body = TextToSend, Sender = App.LoggedInUser, SenderId = App.LoggedInUser.ID });
                    TextToSend = string.Empty;
                }

            });
            

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
