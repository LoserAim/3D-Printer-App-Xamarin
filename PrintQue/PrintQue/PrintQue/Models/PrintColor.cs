using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public static async Task<int> Insert(PrintColor printcolor)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            var rows = await conn.InsertAsync(printcolor);
            await conn.CloseAsync();
            return rows;
        }
    }
}
