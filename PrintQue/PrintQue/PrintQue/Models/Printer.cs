using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [ForeignKey(typeof(Status))]
        public int StatusID { get; set; }
        [ForeignKey(typeof(PrintColor))]
        public int ColorID { get; set; }
        public int ProjectsQueued { get; set; }
        [ManyToOne]
        public Status status { get; set; }
        [ManyToOne]
        public PrintColor printColor { get; set;}

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Request> requests { get; set; }

        public Printer()
        {
            this.requests = new List<Request>();
        }

        public static async Task Insert(Printer printer)
        {
            var status = printer.status;
            var printColor = printer.printColor;
            status.printers.Add(printer);
            printColor.printers.Add(printer);

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            await conn.InsertAsync(printer);
            await conn.UpdateWithChildrenAsync(status);
            await conn.UpdateWithChildrenAsync(printColor);
            
            
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


    }
}
