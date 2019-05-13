using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class PrintColorViewModel : PrintColor
    {

        public List<Printer> printers { get; set; } = new List<Printer>();

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
                usersviewmodel.Add(inser);
            }
            //ADD PULL OF FOREIGN KEYS
            return usersviewmodel;

            return printColors;
        }
        public static async Task<PrintColor> SearchByID(string ID)
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
