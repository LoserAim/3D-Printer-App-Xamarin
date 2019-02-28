using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserMainPage : ContentPage
	{
        List<Printer> GetPrinters()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Printer>();


                var printers = conn.Table<Printer>().ToList();
                return printers;
            }
        }
        public UserMainPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            PrinterListView.ItemsSource = GetPrinters();
        }
        private void PrinterStatusButton_Clicked(object sender, EventArgs e)
        {

        }
        private void PrinterListView_Refreshing(object sender, EventArgs e)
        {
            PrinterListView.ItemsSource = GetPrinters();
            PrinterListView.IsRefreshing = false;
            PrinterListView.EndRefresh();
        }
        private void CreateRequestButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserPages.UserSubmitRequestPage());
        }
    }
}