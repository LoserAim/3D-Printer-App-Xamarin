using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            bool isNameEmpty = string.IsNullOrEmpty(NameEntry.Text);
            bool isUsernameEmpty = string.IsNullOrEmpty(userNameEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(userPasswordEntry.Text);
            bool isConfirmEmpty = string.IsNullOrEmpty(PasswordConfirmEntry.Text);

            if (isUsernameEmpty || isPasswordEmpty || isNameEmpty || isConfirmEmpty)
            {
                //then show error
                DisplayAlert("Attention", "Please fill out all forms", "ok");
            }
            else
            {
                //then try register user
                if (!userPasswordEntry.Text.Equals(PasswordConfirmEntry.Text))
                {
                    //passwords are not matching error

                    DisplayAlert("Attention", "Passwords do not match", "ok");
                } 
                 else
                {
                    //then register the user

                    User user = new User()
                    {
                        Name = NameEntry.Text,
                        Email = userNameEntry.Text,
                        Admin = 0,
                        Password = PasswordConfirmEntry.Text
                    };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<User>();
                        var Num = conn.Insert(user);
                        if(Num >0)
                        {
                            DisplayAlert("Success", "You have been Registered", "OK");
                            Navigation.PushAsync(new LoginPage());
                        }
                        else
                        {
                            DisplayAlert("Failure", "You Have not been Registered", "OK");
                        }
                    }
                }

            }
        }
    }
}