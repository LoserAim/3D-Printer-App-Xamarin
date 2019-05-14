using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class PrintColorViewModel : PrintColor
    {

        public List<PrinterViewModel> Printers { get; set; } = new List<PrinterViewModel>();

        public static async Task Insert(PrintColorViewModel printColorViewModel)
        {
            var printColor = new PrintColor()
            {
                Name = printColorViewModel.Name,
                HexValue = printColorViewModel.HexValue,

            };
            await App.MobileService.GetTable<PrintColor>().InsertAsync(printColor);
        }
        public static async Task<List<PrintColorViewModel>> GetAll()
        {
            List<PrintColor> printColors = new List<PrintColor>();
            List<PrintColorViewModel> printColorsViewModel = new List<PrintColorViewModel>();
            printColors = await App.MobileService.GetTable<PrintColor>().ToListAsync();
            foreach (var pc in printColors)
            {
                var inser = new PrintColorViewModel()
                {
                    ID = pc.ID,
                    Name    =  pc.Name,
                    HexValue = pc.HexValue,
                };
                if (inser.ID != null)
                    inser.Printers = PrinterViewModel.GetAll().Result.ToList().Where(p => p.ColorID.Contains(inser.ID)).ToList();
                printColorsViewModel.Add(inser);
            }

            return printColorsViewModel;
        }
        public static async Task<PrintColorViewModel> SearchByID(string ID)
        {
            List<PrintColorViewModel> printColors = await GetAll();
            return printColors.FirstOrDefault(u => u.ID == ID);

        }
        public static async Task<PrintColorViewModel> SearchByName(string searchText = null)
        {
            List<PrintColorViewModel> printColors = await GetAll();

            return printColors.FirstOrDefault(g => g.Name == searchText);
        }
    }
}
