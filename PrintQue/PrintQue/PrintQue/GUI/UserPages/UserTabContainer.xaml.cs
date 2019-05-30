using PrintQue.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserTabContainer : TabbedPage
	{

		public UserTabContainer ()
		{
			InitializeComponent ();

		}
        async private void ToolbarItem_Run_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "You are about to logout. Are you sure?", "Yes", "No");
            if (response)
            {
                Preferences.Set("RememberMe", false);
                SecureStorage.Remove("User_Email");
                SecureStorage.Remove("User_Password");
                App.LoggedInUser = null;
                await Navigation.PopAsync();
            }
        }

    }
}