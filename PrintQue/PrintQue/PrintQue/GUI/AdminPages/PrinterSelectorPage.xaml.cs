using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.AdminPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrinterSelectorPage : ContentPage
	{
        List<Printer> GetPrinters()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {



                return conn.GetAllWithChildren<Printer>().ToList();
            }
        }
        public PrinterSelectorPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in GetPrinters())
            {
                StringList.Add(p.Name);
            }
            Printer_ListView.ItemsSource = StringList;
        }
        public ListView PrinterNames { get { return Printer_ListView; } }

    }
}