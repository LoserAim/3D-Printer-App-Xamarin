using Microsoft.WindowsAzure.MobileServices;
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
                ID = printColorViewModel.ID,
                Name = printColorViewModel.Name,
                HexValue = printColorViewModel.HexValue,

            };
            
            await App.printColorsTable.InsertAsync(printColor);
            //await App.MobileService.SyncContext.PushAsync();
        }
        public static async Task<List<PrintColorViewModel>> GetAll()
        {
            List<PrintColor> printColors = new List<PrintColor>();
            List<PrintColorViewModel> printColorsViewModel = new List<PrintColorViewModel>();
            printColors = await App.printColorsTable.ToListAsync();
            foreach (var pc in printColors)
            {
                var inser = new PrintColorViewModel()
                {
                    ID = pc.ID,
                    Name    =  pc.Name,
                    HexValue = pc.HexValue,
                };
                printColorsViewModel.Add(inser);
            }

            return printColorsViewModel;
        }
        private static List<PrintColorViewModel> ReturnListPrintColorViewModel(List<PrintColor> printColors)
        {
            List<PrintColorViewModel> printColorsViewModel = new List<PrintColorViewModel>();
            foreach (var pc in printColors)
            {
                var inser = new PrintColorViewModel()
                {
                    ID = pc.ID,
                    Name = pc.Name,
                    HexValue = pc.HexValue,
                };
                printColorsViewModel.Add(inser);
            }
            return printColorsViewModel;
        }
        private static PrintColorViewModel ReturnPrintColorViewModel(PrintColor printColor)
        {
            PrintColorViewModel printColorViewModel = new PrintColorViewModel()
            {
                ID = printColor.ID,
                Name = printColor.Name,
                HexValue = printColor.HexValue,
            };
            return printColorViewModel;
        }
        public static async Task<PrintColorViewModel> SearchByID(string ID)
        {
            PrintColor printColor = (await App.printColorsTable.Where(pc => pc.ID.Contains(ID)).ToListAsync()).FirstOrDefault();
            return ReturnPrintColorViewModel(printColor);

        }
        public static async Task<PrintColorViewModel> SearchByName(string searchText = null)
        {
            if(searchText != null)
            {
                PrintColor printColor = (await App.printColorsTable.Where(pc => pc.Name.Contains(searchText)).ToListAsync()).FirstOrDefault();
                return ReturnPrintColorViewModel(printColor);
            }
            else
            {
                return null;
            }
            
        }
    }
}
