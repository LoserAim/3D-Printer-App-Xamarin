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


        public static async Task Insert(PrinterViewModel printerViewModel)
        {
            var printer = new Printer()
            {
                Name = printerViewModel.Name,
                StatusID = printerViewModel.StatusID,
                ColorID = printerViewModel.ColorID,
                ProjectsQueued = printerViewModel.ProjectsQueued,

            };
            await App.MobileService.GetTable<Printer>().InsertAsync(printer);
        }
        public static async Task<List<PrinterViewModel>> GetAll()
        {
            List<Printer> printers = new List<Printer>();
            List<PrinterViewModel> printersviewmodel = new List<PrinterViewModel>();
            printers = await App.MobileService.GetTable<Printer>().ToListAsync();
            foreach (var p in printers)
            {
                var printer = new PrinterViewModel()
                {
                    ID     = p.ID,
                    Name = p.Name,
                    StatusID = p.StatusID,
                    ColorID = p.ColorID,
                    ProjectsQueued = p.ProjectsQueued,

                };
                if(printer.StatusID != null)
                {
                    printer.Status = await StatusViewModel.SearchByID(printer.StatusID);
                }
                if (printer.ColorID != null)
                {
                    printer.PrintColor = await PrintColorViewModel.SearchByID(printer.ColorID);
                }
                var requests = await RequestViewModel.SearchByPrinter(printer);
                if(requests != null)
                {
                    printer.Requests = requests;
                }
                printersviewmodel.Add(printer);
            }

            return printersviewmodel;
        }
        public static async Task<PrinterViewModel> SearchByName(string searchText = null)
        {
            List<PrinterViewModel> printers = await GetAll();

            return printers.FirstOrDefault(g => g.Name == searchText);
        }

        public static async Task<PrinterViewModel> SearchByID(string ID)
        {
            List<PrinterViewModel> printers = await GetAll();
            return printers.FirstOrDefault(u => u.ID == ID);

        }

    }
}
