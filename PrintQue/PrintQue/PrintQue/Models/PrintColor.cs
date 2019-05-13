using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.Models
{
    public class PrintColor
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string HexValue { get; set; }


    }
}
