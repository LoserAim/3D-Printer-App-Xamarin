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
		public UserMainPage ()
		{
			InitializeComponent ();
		}

        private void PrinterStatusButton_Clicked(object sender, EventArgs e)
        {

        }

        private void CreateRequestButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserPages.UserSubmitRequestPage());
        }
        async private void ToolbarItem_Run_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "You are about to logout. Are you sure?", "Yes", "No");
            if (response)
                await Navigation.PopAsync();

        }
    }
}