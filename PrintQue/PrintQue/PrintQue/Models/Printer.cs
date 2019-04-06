using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int ProjectsQueued { get; set; }
<<<<<<< HEAD
        public static async Task<int> Insert(Printer printer)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            var rows = await conn.InsertAsync(printer);
            
            return rows;
        }
        public static async Task<List<Printer>> GetAll()
        {
            List<Printer> printers = new List<Printer>();
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            await conn.CreateTableAsync<Printer>();

            printers = await conn.Table<Printer>().ToListAsync();

            
            return printers;
        }
        public static async Task<Printer> SearchByName(string searchText = null)
        {
            List<Printer> printers = await GetAll();

            return printers.FirstOrDefault(g => g.Name == searchText);
        }
        public static async Task<Status> GetChildStatus(Printer printer)
        {
            return await Status.SearchByID(printer.StatusID);
        }

        public static async Task<PrintColor> GetChildPrintColor(Printer printer)
        {
            return await PrintColor.SearchByID(printer.ColorID);
        }
        public static async Task<Printer> SearchByID(int ID)
        {
            List<Printer> printers = await GetAll();
            return printers.FirstOrDefault(u => u.ID == ID);

        }
=======
>>>>>>> parent of e8b7215... Implementing async features

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Request> Requests { get; set; }
        [ManyToOne]
        public Status status { get; set; }

        [ManyToOne]
        public PrintColor color { get; set; }
    }
}
