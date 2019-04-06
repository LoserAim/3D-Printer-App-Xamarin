using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
            return rows;
        }
        public static async Task<List<PrintColor>> GetAll()
        {
            List<PrintColor> printColors = new List<PrintColor>();
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            await conn.CreateTableAsync<PrintColor>();

            printColors = await conn.Table<PrintColor>().ToListAsync();

            return printColors;
        }
        public static async Task<PrintColor> SearchByID(int ID)
        {
            List<PrintColor> printColors = await GetAll();
            return printColors.FirstOrDefault(u => u.ID == ID);

        }
        public static async Task<PrintColor> SearchByName(string searchText = null)
        {
            List<PrintColor> printColors = await GetAll();

            return printColors.FirstOrDefault(g => g.Name == searchText);
        }

    }
}
