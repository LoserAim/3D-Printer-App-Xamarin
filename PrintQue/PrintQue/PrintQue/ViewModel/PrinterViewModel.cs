using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.ViewModel
{
    public class PrinterViewModel : Printer
    {

        public Status Status { get; set; } = new Status();
        public PrintColor PrintColor { get; set; } = new PrintColor();
        public List<Request> Requests { get; set; } = new List<Request>();


        public static async Task Insert(Printer printer)
        {
            var status = printer.Status;
            var printColor = printer.PrintColor;
            status.Printers.Add(printer);
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
            printers = await conn.GetAllWithChildrenAsync<Printer>();
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
        public static async Task<Printer> SearchByID(string ID)
        {
            List<Printer> printers = await GetAll();
            return printers.FirstOrDefault(u => u.ID == ID);

        }

    }
}
