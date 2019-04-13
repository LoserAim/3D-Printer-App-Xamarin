using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrintQue.Models
{
    class MessageViewModel : BaseMessageViewModel
    {
        public ObservableCollection<Message> messages { get; set; }

        public MessageViewModel()
        {
            this.messages = new ObservableCollection<Message>();

            //test

            MessageHub MockMessageHub = new MessageHub();
            MockMessageHub.Email = "mock@gmail.com";
            MockMessageHub.HubTitle = "Mock Message Hub";
            MockMessageHub.ID = 1;
            MockMessageHub.Messages = messages;

            this.messages.Add(new Message
            {
                ID = MockMessageHub.ID,
                MessageHub = MockMessageHub,
                MessageHubId = 1,
                Sender = "Sender #1",
                Body = "Message #1",
                TimeSent = DateTime.Now
            });

            this.messages.Add(new Message
            {
                ID = MockMessageHub.ID,
                MessageHub = MockMessageHub,
                MessageHubId = 1,
                Sender = "Sender #1",
                Body = "Message #2",
                TimeSent = DateTime.Now
            });

            //end test
        }
    }
}
