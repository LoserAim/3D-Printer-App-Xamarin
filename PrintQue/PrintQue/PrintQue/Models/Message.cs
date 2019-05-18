using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrintQue.ViewModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync;
using SQLiteNetExtensionsAsync.Extensions;

namespace PrintQue.Models
{
    public class Message
    {
        public string ID { get; set; }
        public string SenderId { get; set; }
        public string RequestId { get; set; }
        public DateTime TimeSent { get; set; }
        public string Body { get; set; }

    }


}

