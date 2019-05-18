using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintQue.Models
{
    public class Printer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string StatusID { get; set; }
        public string ColorID { get; set; }
        public int ProjectsQueued { get; set; }

    }
}
