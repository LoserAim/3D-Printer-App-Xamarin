using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class Status
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Unique,NotNull,MaxLength(10)]
        public string Name { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Printer> Printers { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Request> Requests { get; set; }
    }
}
