using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    class Mail
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Sender { get; set; }
        public string Body { get; set; }
        public string Receiver { get; set; }
        public void SendEmail(string sender, string body, string receiver)
        {
            this.Sender = sender;
            this.Body = body;
            this.Receiver = receiver;
            Send();
        }
        public void SendEmail(Message message, string receiver)
        {
            this.Sender = message.Sender;
            this.Receiver = receiver;
            this.Body = message.Body;
            Send();
        }
        public void Send()
        {
            //Send the email with the class variables
            //Sender: The user who is sending the email.
            //Body: The message that will be displayed in the email.
            //Receiver: The user that will be receiving the email.
        }
    }
}
