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
        public string SenderID { get; set; }
        public string RequestID { get; set; }
        public DateTime Sent { get; set; }
        public string Body { get; set; }

    }


}

