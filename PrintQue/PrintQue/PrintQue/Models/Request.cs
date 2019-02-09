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
        public int RequestID { get; set; }
        [MaxLength(8)]
        public string Status { get; set; }
        [ForeignKey(typeof(Printer))]
        public int PrinterID { get; set; }
        [ForeignKey(typeof(User))]
        public int UserID { get; set; }
        public DateTime DateRequestMade { get; set; }
        public DateTime DateRequestSet { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int ProjectType { get; set; }
        [ManyToOne]
        public User user { get; set; }
        [ManyToOne]
        public Printer printer { get; set; }
    }
}
