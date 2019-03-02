using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.AdminPages.DetailPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrintColorDetailPage : ContentPage
	{
		public PrintColorDetailPage ()
		{
			InitializeComponent ();
		}
        private void ToolbarItem_Save_Activated(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<PrintColor>();
                var printcolor = new PrintColor()
                {
                    Name = ent_Name.Text,
                    HexValue = ent_HexValue.Text,
                };
                var rows = conn.Insert(printcolor);
                if (rows > 0)
                    DisplayAlert("Success!", "Print Color was successfully save!", "OK");
                else
                    DisplayAlert("Failure", "Print Color was not saved!", "OK");


            }
        }
    }
}