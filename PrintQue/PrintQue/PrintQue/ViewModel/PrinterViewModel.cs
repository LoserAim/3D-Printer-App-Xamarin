using Microsoft.WindowsAzure.MobileServices;
using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class PrinterViewModel : Printer
    {

        public StatusViewModel Status { get; set; } = new StatusViewModel();
        public PrintColorViewModel PrintColor { get; set; } = new PrintColorViewModel();
        public List<RequestViewModel> Requests { get; set; } = new List<RequestViewModel>();

        public static async Task<PrinterViewModel> PopulateForeignKeys(PrinterViewModel printer)
        {
            if (printer.StatusID != null)
            {
                printer.Status = await StatusViewModel.SearchByID(printer.StatusID);
            }
            if (printer.ColorID != null)
            {
                printer.PrintColor = await PrintColorViewModel.SearchByID(printer.ColorID);
            }
            var requests = await RequestViewModel.SearchByPrinter(printer);
            if (requests != null)
            {
                printer.Requests = requests;
            }
            return printer;
        }
        public static async Task Insert(PrinterViewModel printerViewModel)
        {
            var printer = new Printer()
            {
                ID = printerViewModel.ID,
                Name = printerViewModel.Name,
                StatusID = printerViewModel.StatusID,
                ColorID = printerViewModel.ColorID,
                ProjectsQueued = printerViewModel.ProjectsQueued,

            };
            await App.printersTable.InsertAsync(printer);
            await App.MobileService.SyncContext.PushAsync();
        }
        public static async Task<List<PrinterViewModel>> GetAll()
        {
            List<Printer> printers = new List<Printer>();
            List<PrinterViewModel> printersviewmodel = new List<PrinterViewModel>();
            printers = await App.printersTable.ToListAsync();
            foreach (var p in printers)
            {
                var item = ReturnPrinterViewModel(p);
                item = await PopulateForeignKeys(item);
                printersviewmodel.Add(item);
               
            }

            return printersviewmodel;
        }
        private static PrinterViewModel ReturnPrinterViewModel(Printer printer)
        {
            var printerviewmodel = new PrinterViewModel()
            {
                ID = printer.ID,

                Name = printer.Name,
                StatusID = printer.StatusID,
                ColorID = printer.ColorID,
                ProjectsQueued = printer.ProjectsQueued,

            };
            return printerviewmodel;
        }
        public static async Task<PrinterViewModel> SearchByName(string searchText = null)
        {
            Printer sorted = (await App.printersTable.Where(u => u.Name.Contains(searchText)).ToListAsync()).FirstOrDefault();
            return ReturnPrinterViewModel(sorted);
        }

        public static async Task<PrinterViewModel> SearchByID(string ID)
        {
            Printer sorted = (await App.printersTable.Where(u => u.ID.Contains(ID)).ToListAsync()).FirstOrDefault();
            return ReturnPrinterViewModel(sorted);

        }

    }
}
