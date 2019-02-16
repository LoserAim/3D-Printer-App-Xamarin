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
	public partial class RequestsPage : ContentPage
	{
        public int ItemSelected;
		public RequestsPage ()
		{
			InitializeComponent ();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Request>();
                var requests = conn.Table<Request>().ToList();
                if (requests.Count < 1)
                {
                    var TestRequest = new Request()
                    {
                        ProjectName = "BubbyHitMeUp",
                        UserID = 1,
                        PrinterID = 1,

                    };
                    conn.Insert(TestRequest);
                }
                

            }
        }



        private void ClickedMenuDeny(object sender, EventArgs e)
        {
            try
            {


            }
            catch
            {

            }
        }
        private void ClickedMenuDetails(object sender, EventArgs e)
        {
            try
            {


            }
            catch
            {

            }
        }
        protected override void OnAppearing()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Request>();
                var requests = conn.Table<Request>().ToList();
                RequestListView.ItemsSource = requests;

            }
        }

        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}