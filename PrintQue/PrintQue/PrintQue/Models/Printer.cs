using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.Models
{
    public class Printer
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        [MaxLength(50), Unique]
        public string Name { get; set; }
        public int StatusID { get; set; }
        public int ColorID { get; set; }
        public int ProjectsQueued { get; set; }
        public static async Task<int> Insert(Printer printer)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            var rows = await conn.InsertAsync(printer);
            await conn.CloseAsync();
            return rows;
        }


    }
}
