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

        public List<Printer> Printers { get; set; } = new List<Printer>();

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
                    Name    =  pc.Name,
                    HexValue = pc.HexValue,
                };
                printColorsViewModel.Add(inser);
            }
            //ADD PULL OF FOREIGN KEYS
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
