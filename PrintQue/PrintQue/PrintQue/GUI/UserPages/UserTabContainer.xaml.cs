using PrintQue.GUI.UserPages;
using PrintQue.ViewModel;
using PrintQue.Widgets.ChatWidget;
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
        //private async void isEmpty()
        //{
        //    var page = new ChatPage();
        //    var page1 = new UserSubmissionsPage();
        //    if ((await RequestViewModel.GetAll()).Count == 0)
        //    {
        //        UserTabbedPage.Children.RemoveAt(1);
        //        UserTabbedPage.Children.RemoveAt(1);
        //    }
        //    else if(!UserTabbedPage.Children.Contains(page1))
        //    {
        //        UserTabbedPage.Children.Add(page1);
        //        if (!UserTabbedPage.Children.Contains(page))
        //        {
        //            UserTabbedPage.Children.Add(page);
        //        }
        //    }

        //}

        public UserTabContainer ()
		{
			InitializeComponent ();

		}
        protected override void OnAppearing()
        {
            //isEmpty();

            base.OnAppearing();
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