using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class MessageHub
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(50), Unique]
        public string Email { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        //public string MessageBody { get; set; } 
        public DateTime LatestMsg { get; set; }
        public string HubTitle { get; set; }
    }
    public class Message
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ManyToOne]
        public MessageHub MessageHub { get; set; }
        [ForeignKey(typeof(MessageHub))]
        public int MessageHubId { get; set; }
        public string Sender { get; set; }
        public string Body { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
