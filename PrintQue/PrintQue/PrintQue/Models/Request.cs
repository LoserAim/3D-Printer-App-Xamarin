using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace PrintQue.Models
{
    public class Request
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(Printer))]
        public int PrinterID { get; set; }
        [ForeignKey(typeof(Status))]
        public int StatusID { get; set; }
        [ForeignKey(typeof(User))]
        public int UserID { get; set; }
        public DateTime DateMade { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime Duration { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int Personal { get; set; }
        [ManyToOne]
        public User user { get; set; }
        [ManyToOne]
        public Status status { get; set; }
        [ManyToOne]
        public Printer printer { get; set; }
    }
}
