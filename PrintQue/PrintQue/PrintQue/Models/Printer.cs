using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class Printer
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        [MaxLength(50), Unique]
        public string Name { get; set; }
        [NotNull, MaxLength(10)]
        public string Status { get; set; }
        [NotNull]
        public string Color { get; set; }
        public int ProjectsQueued { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Request> Requests { get; set; }
    }
}
