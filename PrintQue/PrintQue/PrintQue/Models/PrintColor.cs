using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class PrintColor
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Unique, NotNull]
        public string Name { get; set; }
        [Unique, NotNull]

        public string HexValue { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Printer> Printers { get; set; }
    }
}
