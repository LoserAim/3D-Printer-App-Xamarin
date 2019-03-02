using PrintQue.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
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
                if(requests.Count > 0)
                {
                    conn.DeleteAll<Request>();
                }
                
                
                var users = conn.Table<User>().ToList();
                var printers = conn.Table<Printer>().ToList();
                var UserRequest = users.SingleOrDefault(g => g.UserID == 1);
                var PrinterRequest = printers.SingleOrDefault(p => p.ID == 1);
                if (requests.Count < 1)
                {
                    var TestRequest = new Request()
                    {
                        ProjectName = "BubbyHitMeUp",
                        //user = UserRequest,
                        //printer = PrinterRequest,
                        //UserID = UserRequest.UserID,
                        //PrinterID = PrinterRequest.ID,

                    };
                    conn.Insert(TestRequest);
                    
                    TestRequest = new Request()
                    {
                        ProjectName = "What the frick",

                    };
                    conn.Insert(TestRequest);
                    TestRequest = new Request()
                    {
                        ProjectName = "Manassa",

                    };
                    conn.Insert(TestRequest);
                    TestRequest = new Request()
                    {
                        ProjectName = "ButtBaby",

                    };
                    conn.Insert(TestRequest);
                }
                requests = conn.Table<Request>().ToList();
                foreach (var r in requests)
                {
                    UserRequest.Requests.Add(r);
                    PrinterRequest.Requests.Add(r);
                }
                conn.UpdateWithChildren(UserRequest);
                conn.UpdateWithChildren(PrinterRequest);



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