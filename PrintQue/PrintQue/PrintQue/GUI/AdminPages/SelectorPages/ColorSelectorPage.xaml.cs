using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.AdminPages.SelectorPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ColorSelectorPage : ContentPage
	{
		public ColorSelectorPage ()
		{
			InitializeComponent ();
		}
        List<PrintColor> GetPrintColors()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<PrintColor>();


                return conn.Table<PrintColor>().ToList();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in GetPrintColors())
            {
                StringList.Add(p.Name);
                Color_ListView.ItemsSource = StringList;
            }
        }
        public ListView ColorNames { get { return Color_ListView; } }
    }
}