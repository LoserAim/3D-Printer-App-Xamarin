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

        public Status Status { get; set; } = new Status();
        public PrintColor PrintColor { get; set; } = new PrintColor();
        public List<Request> Requests { get; set; } = new List<Request>();


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
                    Name = p.Name,
                    StatusID = p.StatusID,
                    ColorID = p.ColorID,
                    ProjectsQueued = p.ProjectsQueued,

                };
                printersviewmodel.Add(printer);
            }
            //ADD PULL OF FOREIGN KEYS
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
