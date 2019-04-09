using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class MessageHub
    {
        public int messageHubId { get; set; }
        [MaxLength(50)]
        public string email { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        //public string MessageBody { get; set; } 
        public DateTime latestMsg { get; set; }
        public string hubTitle { get; set; }
    }
    public class Message
    {
        public int messageHubId { get; set; }
        public MessageHub messageHub { get; set; }
        public int messageId { get; set; }
        public string sender { get; set; }
        public string body { get; set; }
        public DateTime timeSent { get; set; }
    }
}
