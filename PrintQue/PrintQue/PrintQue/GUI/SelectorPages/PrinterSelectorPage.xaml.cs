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
using PrintQue.ViewModel;

namespace PrintQue.GUI.SelectorPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrinterSelectorPage : ContentPage
	{
        public PrinterSelectorPage ()
		{
			InitializeComponent ();
		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in await PrinterViewModel.GetAll())
            {
                StringList.Add(p.Name);
            }
            Printer_ListView.ItemsSource = StringList;
        }
        public ListView PrinterNames { get { return Printer_ListView; } }

    }
}