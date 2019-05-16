using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

using System.Threading.Tasks;
using System.Linq;


namespace PrintQue.Models
{
    public class Request
    {
        public string    ID             { get; set; }
        public string   PrinterID       { get; set; }
        public string   StatusID        { get; set; }
        public string   ApplicationUserID          { get; set; }
        public DateTime DateMade        { get; set; }
        public DateTime DateRequested   { get; set; }
        public int      Duration        { get; set; }
        public string   ProjectName     { get; set; }
        public string   Description     { get; set; }
        public string   File            { get; set; }
        public string   Personal        { get; set; }

    }
}
