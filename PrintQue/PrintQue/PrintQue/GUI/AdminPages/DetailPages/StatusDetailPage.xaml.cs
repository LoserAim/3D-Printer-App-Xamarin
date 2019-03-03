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
	public partial class StatusDetailPage : ContentPage
	{
		public StatusDetailPage ()
		{
			InitializeComponent ();
		}

        private void ToolbarItem_Save_Activated(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Status>();
                var status = new Status()
                {
                    Name = ent_Name.Text,
                };
                var rows = conn.Insert(status);
                if (rows > 0)
                {
                    DisplayAlert("Success!", "Status was successfully save!", "OK");
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Failure", "Status was not saved!", "OK");
                }


            }
        }
    }
}