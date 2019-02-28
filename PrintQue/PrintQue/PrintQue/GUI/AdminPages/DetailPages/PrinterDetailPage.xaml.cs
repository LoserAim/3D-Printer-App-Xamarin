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
        private Printer _printer = new Printer();
		public PrinterDetailPage ()
		{
			InitializeComponent ();
            BindingContext = _printer;
		}
        private void Clicked_Done(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Printer>();

                
            }
        }
        
    }
}