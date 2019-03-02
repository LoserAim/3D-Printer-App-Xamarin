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
        [ForeignKey(typeof(Status))]
        public int StatusID { get; set; }
        [ForeignKey(typeof(PrintColor))]
        public int ColorID { get; set; }
        [NotNull]
        public string Color { get; set; }
        public int ProjectsQueued { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Request> Requests { get; set; }
        [ManyToOne]
        public Status status { get; set; }

        [ManyToOne]
        public PrintColor color { get; set; }
    }
}
