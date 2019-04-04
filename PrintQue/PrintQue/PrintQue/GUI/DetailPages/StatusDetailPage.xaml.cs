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

        private async void ToolbarItem_Save_Activated(object sender, EventArgs e)
        {
            var exists = await Status.SearchByName(ent_Name.Text);
            if (exists == null)
            {
                var status = new Status()
                {
                    Name = ent_Name.Text,
                };
                int rows = await Status.Insert(status);
                if (rows > 0)
                {
                    await DisplayAlert("Success!", "Status was successfully save!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Failure", "Status was not saved!", "OK");
                }
            }
            else
            {
                await DisplayAlert("ERROR", "Name already Used. Please choose another", "OK");
            }
        }
    }
}