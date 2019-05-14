using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensionsAsync.Extensions;

namespace PrintQue.Models
{

    public class Status
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }
}
