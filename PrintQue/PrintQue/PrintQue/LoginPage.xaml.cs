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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(userNameEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(userPasswordEntry.Text);

            if (isUsernameEmpty || isPasswordEmpty)
            {
                //then show error

            }
            else
            {
                //then try to log in user
                if (userNameEntry.Text.Equals("admin")){
                    //admin
                    Navigation.PushAsync(new AdminTabContainer());
                } else
                {
                    //student
                    Navigation.PushAsync(new UserTabContainer());
                }

                
            }

        }
    }
}