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
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Admin { get; set; }
        public string Password { get; set; }
    }
}
