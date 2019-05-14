using PrintQue.Models;
using PrintQue.ViewModel;
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
	public partial class PrintColorDetailPage : ContentPage
	{
		public PrintColorDetailPage ()
		{
			InitializeComponent ();
		}
        private async void ToolbarItem_Save_Activated(object sender, EventArgs e)
        {
            var exists = await PrintColorViewModel.SearchByName(ent_Name.Text);
            if (exists == null)
            {
                var printcolor = new PrintColorViewModel()
                {
                    Name = ent_Name.Text,
                    HexValue = ent_HexValue.Text,
                };
                await PrintColorViewModel.Insert(printcolor);

                await DisplayAlert("Success!", "Print Color was successfully save!", "OK");
                await Navigation.PopAsync();

            }
            else
            {
                await DisplayAlert("ERROR", "Name already Used. Please choose another", "OK");

            }

        }
    }
}