using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using SQLiteNetExtensionsAsync.Extensions;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PrintQue.Models
{
    public class User
    {
        public string ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime LatestMessage { get; set; }
        public int Admin { get; set; }

    }
}
