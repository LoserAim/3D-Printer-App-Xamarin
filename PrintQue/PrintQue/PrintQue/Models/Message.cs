using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    class Message
    {
        [PrimaryKey, AutoIncrement]
        public int MessageId { get; set; }
        public User Sender { get; set; }
        [ForeignKey(typeof(User))]
        public int SenderId { get; set; }
        public string Body { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
