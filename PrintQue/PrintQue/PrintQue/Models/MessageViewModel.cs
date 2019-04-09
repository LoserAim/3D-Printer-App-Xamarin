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
            MockMessageHub.email = "mock@gmail.com";
            MockMessageHub.hubTitle = "Mock Message Hub";
            MockMessageHub.messageHubId = 1;
            MockMessageHub.Messages = messages;

            this.messages.Add(new Message
            {
                messageHubId = MockMessageHub.messageHubId,
                messageHub = MockMessageHub,
                messageId = 1,
                sender = "Sender #1",
                body = "Message #1",
                timeSent = DateTime.Now
            });

            this.messages.Add(new Message
            {
                messageHubId = MockMessageHub.messageHubId,
                messageHub = MockMessageHub,
                messageId = 1,
                sender = "Sender #1",
                body = "Message #2",
                timeSent = DateTime.Now
            });

            //end test
        }
    }
}
