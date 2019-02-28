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
	public partial class PrinterDetailPage : ContentPage
	{

		public PrinterDetailPage ()
		{
			InitializeComponent ();

		}
        private void ToolbarItem_Save_Activated(object sender, EventArgs e)
        {
            var printer = new Printer()
            {
                Name = ent_Name.Text,
                Status = ent_Status.Text,
                Color  = ent_Color.Text,
                ProjectsQueued = 0,
            };
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Printer>();
                conn.Insert(printer);
            }

        }
    }
}