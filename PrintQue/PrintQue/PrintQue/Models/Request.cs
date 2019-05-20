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
        public string ID { get; set; }
        public string ApplicationUserId { get; set; }
        public string PrinterId { get; set; }
        public string StatusId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectFilePath { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateMade { get; set; }
        public string ProjectDescript { get; set; }
        public bool PersonalUse { get; set; }
        public double Duration { get; set; }


    }
}
