using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.DetailPages
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
            var status = new Status()
            {
                Name = ent_Name.Text,
            };
            int rows = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
         
                rows = conn.Insert(status);
            }
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